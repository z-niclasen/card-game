namespace CardGameCore.Framework.CombatEncounter;

public delegate void DrawCardDelegate(ICombatCardCollection collectionMutable, ICard card);
public delegate void DiscardCardDelegate(ICombatCardCollection collectionMutable, ICard card);
public delegate void ExhaustCardDelegate(ICombatCardCollection collectionMutable, ICard card);
public delegate void ShuffleIntoDrawPileDelegate(ICombatCardCollection collectionMutable);
public delegate void DrawPileChangedDelegate(ICombatCardCollection collectionMutable);
public delegate void DiscardPileChangedDelegate(ICombatCardCollection collectionMutable);
public delegate void ExhaustPileChangedDelegate(ICombatCardCollection collectionMutable);

public interface ICombatCardCollection
{
    public event DrawCardDelegate? OnDrawCard;
    public event DiscardCardDelegate? OnDiscardCard;
    public event ExhaustCardDelegate? OnExhaustCard;
    public event ShuffleIntoDrawPileDelegate? OnShuffleIntoDrawPile;
    public event DrawPileChangedDelegate? OnDrawPileChanged;
    public event DiscardPileChangedDelegate? OnDiscardPileChanged;
    public event ExhaustPileChangedDelegate? OnExhaustPileChanged;
    
    public int HandCount { get; }
    public int DrawPileCount { get; }
    public int DiscardPileCount { get; }
    public int ExhaustPileCount { get; }

    public IEnumerable<ICard> Hand { get; }
    public IEnumerable<ICard> DiscardPile { get; }
    public IEnumerable<ICard> ExhaustPile { get; }

    public ICard GetCardFromHandAtIndex(int index);
    
    public bool IsCardInHand(ICard card);
}