using CardGameCore.Exceptions;
using CardGameCore.Framework;
using CardGameCore.Framework.CombatEncounter;
using CardGameCore.Utility;

namespace CardGameCore.Impl.CombatEncounter;

public class CombatCardCollectionImpl : ICombatCardCollectionMutable
{
    public event DrawCardDelegate? OnDrawCard;
    public event DiscardCardDelegate? OnDiscardCard;
    public event ExhaustCardDelegate? OnExhaustCard;
    public event ShuffleIntoDrawPileDelegate? OnShuffleIntoDrawPile;
    public event DrawPileChangedDelegate? OnDrawPileChanged;
    public event DiscardPileChangedDelegate? OnDiscardPileChanged;
    public event ExhaustPileChangedDelegate? OnExhaustPileChanged;
    
    public int HandCount => _hand.Count;
    public int DrawPileCount => _drawPile.Count;
    public int DiscardPileCount => _discardPile.Count;
    public int ExhaustPileCount => _exhaustPile.Count;
    
    public IEnumerable<ICard> Hand => _hand;
    public IEnumerable<ICard> DiscardPile => _discardPile;
    public IEnumerable<ICard> ExhaustPile => _exhaustPile;
    
    private List<ICard> _drawPile = [];
    private readonly List<ICard> _hand = [];
    private readonly List<ICard> _discardPile = [];
    private readonly List<ICard> _exhaustPile = [];

    private ShuffleStrategy _shuffleStrategy;

    public CombatCardCollectionImpl(Deck deck, ShuffleStrategy shuffleStrategy)
    {
        _drawPile.AddRange(deck);
        _drawPile = _drawPile.Shuffle(Run.Random).ToList();
        _shuffleStrategy = shuffleStrategy;
    }

    public ICard GetCardFromHandAtIndex(int index)
    {
        if (index < 0)
            throw new ArgumentException("Index cannot be negative.");

        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, HandCount);
        
        return _hand[index];
    }

    public bool IsCardInHand(ICard card)
    {
        return _hand.Contains(card);
    }

    public void DrawCard()
    {
        if (DrawPileCount == 0 && DiscardPileCount == 0)
            return;
        
        if (DrawPileCount == 0)
            AddDiscardToDrawAndShuffle();
        
        ICard drawnCard =  _drawPile[0];
        _drawPile.RemoveAt(0);
        
        _hand.Add(drawnCard);
        
        InvokeOnDrawCard(drawnCard);
    }

    public void DrawNCards(int numberOfCards)
    {
        if (numberOfCards < 0)
            throw new ArgumentException($"Cannot draw {numberOfCards} number of cards.");

        if (numberOfCards == 0)
            return;

        for (int i = 0; i < numberOfCards; i++)
            DrawCard();
    }

    public void DiscardCardFromHand(ICard cardToDiscard)
    {
        if (!_hand.Contains(cardToDiscard))
            throw new CardNotInHandException($"Tried to discard card {cardToDiscard}, but it is not in hand.");
        
        _hand.Remove(cardToDiscard);
        _discardPile.Add(cardToDiscard);

        InvokeOnDiscardCard(cardToDiscard);
    }

    public void DiscardHand()
    {
        while (HandCount > 0)
        {
            ICard firstCardInHand = GetCardFromHandAtIndex(0);
            DiscardCardFromHand(firstCardInHand);
        }
    }

    public void ExhaustCardFromHand(ICard cardToExhaust)
    {
        if (!_hand.Contains(cardToExhaust))
            throw new CardNotInHandException($"Tried to exhaust card {cardToExhaust}, but it is not in hand.");
        
        _hand.Remove(cardToExhaust);
        _exhaustPile.Add(cardToExhaust);
        
        InvokeOnExhaustCard(cardToExhaust);
    }

    private void AddDiscardToDrawAndShuffle()
    {
        _drawPile.AddRange(_discardPile);
        _discardPile.Clear();

        if (_shuffleStrategy == ShuffleStrategy.NoShuffle)
            return;
        
        _drawPile = _drawPile.Shuffle(Run.Random).ToList();

        InvokeOnShuffleIntoDrawPile();
    }

    private void InvokeOnDrawCard(ICard drawnCard)
    {
        OnDrawCard?.Invoke(this, drawnCard);
        OnDrawPileChanged?.Invoke(this);
    }
    
    private void InvokeOnDiscardCard(ICard discardedCard)
    {
        OnDiscardCard?.Invoke(this, discardedCard);
        OnDiscardPileChanged?.Invoke(this);
    }

    private void InvokeOnExhaustCard(ICard exhaustedCard)
    {
        OnExhaustCard?.Invoke(this, exhaustedCard);
        OnExhaustPileChanged?.Invoke(this);
    }

    private void InvokeOnShuffleIntoDrawPile()
    {
        OnShuffleIntoDrawPile?.Invoke(this);
        OnDrawPileChanged?.Invoke(this);
        OnDiscardPileChanged?.Invoke(this);
    }
    
    public enum ShuffleStrategy
    {
        Shuffle, NoShuffle
    }
}