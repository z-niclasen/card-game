using System.Collections;
using CardGameCore.Framework;

namespace CardGameCore.Impl;

public class Deck : IEnumerable<ICard>
{
    public int Count => _cards.Count;
    
    private readonly List<ICard> _cards = new();

    public Deck() { }

    public Deck(IEnumerable<ICard> cards)
    {
        AddCards(cards);
    }

    public void AddCard(ICard card)
    {
        _cards.Add(card);
    }

    public void AddCards(IEnumerable<ICard> cards)
    {
        _cards.AddRange(cards);
    }

    public void RemoveCardAtIndex(int index)
    {
        _cards.RemoveAt(index);
    }
    
    public IEnumerator<ICard> GetEnumerator() => _cards.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _cards.GetEnumerator();
}