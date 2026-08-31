using CardGame.Framework;
using CardGame.Utility;

namespace CardGame.Impl;

public class CombatCardCollection
{
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

    public CombatCardCollection(Deck deck)
    {
        _drawPile.AddRange(deck);
        _drawPile = _drawPile.Shuffle(Run.Random).ToList();
    }

    public ICard GetCardFromHandAtIndex(int index)
    {
        if (index < 0)
            throw new ArgumentException("Index cannot be negative.");

        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, HandCount);
        
        return _hand[index];
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

    public void DiscardCardAtIndex(int index)
    {
        if (index < 0)
            throw new ArgumentException("Index cannot be negative.");

        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, HandCount);
        
        ICard discardedCard =  _hand[index];
        _hand.RemoveAt(index);
        _discardPile.Add(discardedCard);
    }

    public void DiscardHand()
    {
        _discardPile.AddRange(_hand);
        _hand.Clear();
    }

    public void ExhaustCardFromHandAtIndex(int index)
    {
        if (index < 0)
            throw new ArgumentException("Index cannot be negative.");

        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, HandCount);
        
        ICard exhaustedCard = _hand[index];
        _hand.RemoveAt(index);
        _exhaustPile.Add(exhaustedCard);
    }

    private void AddDiscardToDrawAndShuffle()
    {
        _drawPile.AddRange(_discardPile);
        _discardPile.Clear();
        _drawPile = _drawPile.Shuffle(Run.Random).ToList();
    }
}