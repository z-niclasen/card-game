using CardGame.Framework;
using CardGame.Impl;
using CardGame.Library;

namespace CardGame.Test;

public class CombatCardCollectionTest
{
    private Deck _initialDeck;
    private CombatCardCollection _collection;

    [SetUp]
    public void Setup()
    {
        _initialDeck = SteveCards.StarterDeck;
        _collection = new CombatCardCollection(_initialDeck);
    }

    [Test]
    public void CollectionHasCorrectInitialStackCounts()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_collection.DrawPileCount, Is.EqualTo(_initialDeck.Count));
            Assert.That(_collection.HandCount, Is.EqualTo(0));
            Assert.That(_collection.DiscardPileCount, Is.EqualTo(0));
            Assert.That(_collection.ExhaustPileCount, Is.EqualTo(0));
        }
    }
    
    [Test]
    public void CollectionDrawsCardsCorrectly()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_collection.DrawPileCount, Is.EqualTo(_initialDeck.Count));
            Assert.That(_collection.HandCount, Is.EqualTo(0));
            Assert.That(_collection.DiscardPileCount, Is.EqualTo(0));
            Assert.That(_collection.ExhaustPileCount, Is.EqualTo(0));
        }
        
        _collection.DrawCard();
        
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_collection.DrawPileCount, Is.EqualTo(_initialDeck.Count - 1));
            Assert.That(_collection.HandCount, Is.EqualTo(1));
            Assert.That(_collection.DiscardPileCount, Is.EqualTo(0));
            Assert.That(_collection.ExhaustPileCount, Is.EqualTo(0));
        }
        
        const int drawCount = 5;
        _collection.DrawNCards(drawCount - 1);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(_collection.DrawPileCount, Is.EqualTo(_initialDeck.Count - drawCount));
            Assert.That(_collection.HandCount, Is.EqualTo(drawCount));
            Assert.That(_collection.DiscardPileCount, Is.EqualTo(0));
            Assert.That(_collection.ExhaustPileCount, Is.EqualTo(0));
        }
    }

    [Test]
    public void CollectionDiscardsCardsCorrectly()
    {
        const int drawCount = 5;
        _collection.DrawNCards(drawCount);
        
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_collection.DrawPileCount, Is.EqualTo(_initialDeck.Count - drawCount));
            Assert.That(_collection.HandCount, Is.EqualTo(drawCount));
            Assert.That(_collection.DiscardPileCount, Is.EqualTo(0));
            Assert.That(_collection.ExhaustPileCount, Is.EqualTo(0));
        }

        ICard discardedCard = _collection.GetCardFromHandAtIndex(0);
        _collection.DiscardCardAtIndex(0);
        
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_collection.DrawPileCount, Is.EqualTo(_initialDeck.Count - drawCount));
            Assert.That(_collection.HandCount, Is.EqualTo(drawCount - 1));
            Assert.That(_collection.DiscardPileCount, Is.EqualTo(1));
            Assert.That(_collection.ExhaustPileCount, Is.EqualTo(0));
            
            Assert.That(_collection.Hand, Does.Not.Contain(discardedCard));
            Assert.That(_collection.DiscardPile, Does.Contain(discardedCard));
        }

        IEnumerable<ICard> restOfHand = _collection.Hand;
        _collection.DiscardHand();
        
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_collection.DrawPileCount, Is.EqualTo(_initialDeck.Count - drawCount));
            Assert.That(_collection.HandCount, Is.EqualTo(0));
            Assert.That(_collection.DiscardPileCount, Is.EqualTo(drawCount));
            Assert.That(_collection.ExhaustPileCount, Is.EqualTo(0));

            foreach (ICard card in restOfHand)
            {
                Assert.That(_collection.Hand, Does.Not.Contain(card));
                Assert.That(_collection.DiscardPile, Does.Contain(card));
            }
        }
    }

    [Test]
    public void CollectionShufflesDiscardIntoDrawPileCorrectly()
    {
        int initialDrawPileCount = _collection.DrawPileCount;
        
        _collection.DrawNCards(initialDrawPileCount);
        _collection.DiscardHand();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(_collection.DrawPileCount, Is.EqualTo(0));
            Assert.That(_collection.HandCount, Is.EqualTo(0));
            Assert.That(_collection.DiscardPileCount, Is.EqualTo(initialDrawPileCount));
            Assert.That(_collection.ExhaustPileCount, Is.EqualTo(0));
        }
        
        _collection.DrawCard();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(_collection.DrawPileCount, Is.EqualTo(initialDrawPileCount - 1));
            Assert.That(_collection.HandCount, Is.EqualTo(1));
            Assert.That(_collection.DiscardPileCount, Is.EqualTo(0));
            Assert.That(_collection.ExhaustPileCount, Is.EqualTo(0));
        }
    }

    [Test]
    public void CollectionExhaustsCardsCorrectly()
    {
        const int drawCount = 5;
        _collection.DrawNCards(drawCount);
        
        ICard exhaustedCard = _collection.GetCardFromHandAtIndex(0);
        _collection.ExhaustCardFromHandAtIndex(0);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(_collection.DrawPileCount, Is.EqualTo(_initialDeck.Count - drawCount));
            Assert.That(_collection.HandCount, Is.EqualTo(drawCount - 1));
            Assert.That(_collection.DiscardPileCount, Is.EqualTo(0));
            Assert.That(_collection.ExhaustPileCount, Is.EqualTo(1));
            
            Assert.That(_collection.ExhaustPile, Does.Contain(exhaustedCard));
            Assert.That(_collection.Hand, Does.Not.Contain(exhaustedCard));
        }
    }

    [Test]
    public void CollectionDoesNotShuffleExhaustIntoDrawPile()
    {
        int initialDrawPileCount = _collection.DrawPileCount;
        _collection.DrawNCards(initialDrawPileCount);
        
        ICard exhaustedCard1 = _collection.GetCardFromHandAtIndex(0);
        _collection.ExhaustCardFromHandAtIndex(0);
        
        ICard exhaustedCard2 = _collection.GetCardFromHandAtIndex(0);
        _collection.ExhaustCardFromHandAtIndex(0);
        
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_collection.DrawPileCount, Is.EqualTo(0));
            Assert.That(_collection.HandCount, Is.EqualTo(initialDrawPileCount - 2));
            Assert.That(_collection.DiscardPileCount, Is.EqualTo(0));
            Assert.That(_collection.ExhaustPileCount, Is.EqualTo(2));
            
            Assert.That(_collection.ExhaustPile, Does.Contain(exhaustedCard1));
            Assert.That(_collection.ExhaustPile, Does.Contain(exhaustedCard2));
            Assert.That(_collection.Hand, Does.Not.Contain(exhaustedCard1));
            Assert.That(_collection.Hand, Does.Not.Contain(exhaustedCard2));
        }
        
        _collection.DiscardHand();
        
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_collection.DrawPileCount, Is.EqualTo(0));
            Assert.That(_collection.HandCount, Is.EqualTo(0));
            Assert.That(_collection.DiscardPileCount, Is.EqualTo(initialDrawPileCount - 2));
            Assert.That(_collection.ExhaustPileCount, Is.EqualTo(2));
            
            Assert.That(_collection.ExhaustPile, Does.Contain(exhaustedCard1));
            Assert.That(_collection.ExhaustPile, Does.Contain(exhaustedCard2));
        }
        
        _collection.DrawCard();
        
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_collection.DrawPileCount, Is.EqualTo(initialDrawPileCount - 2));
            Assert.That(_collection.HandCount, Is.EqualTo(1));
            Assert.That(_collection.DiscardPileCount, Is.EqualTo(0));
            Assert.That(_collection.ExhaustPileCount, Is.EqualTo(2));
            
            Assert.That(_collection.ExhaustPile, Does.Contain(exhaustedCard1));
            Assert.That(_collection.ExhaustPile, Does.Contain(exhaustedCard2));
            Assert.That(_collection.Hand, Does.Not.Contain(exhaustedCard1));
            Assert.That(_collection.Hand, Does.Not.Contain(exhaustedCard2));
        }
    }

    [Test]
    public void CollectionDoesNothingIfDrawingCardsWithNoDiscardPileAndNoDrawPile()
    {
        int  initialDrawPileCount = _collection.DrawPileCount;
        _collection.DrawNCards(initialDrawPileCount);
        
        for (int i = 0; i < initialDrawPileCount - 5; i++)
            _collection.ExhaustCardFromHandAtIndex(0);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(_collection.DrawPileCount, Is.EqualTo(0));
            Assert.That(_collection.DiscardPileCount, Is.EqualTo(0));
            Assert.That(_collection.HandCount, Is.EqualTo(5));
            Assert.That(_collection.ExhaustPileCount, Is.EqualTo(initialDrawPileCount - 5));
        }
        
        _collection.DrawCard();
        
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_collection.DrawPileCount, Is.EqualTo(0));
            Assert.That(_collection.DiscardPileCount, Is.EqualTo(0));
            Assert.That(_collection.HandCount, Is.EqualTo(5));
            Assert.That(_collection.ExhaustPileCount, Is.EqualTo(initialDrawPileCount - 5));
        }
        
        _collection.DrawNCards(10);
        
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_collection.DrawPileCount, Is.EqualTo(0));
            Assert.That(_collection.DiscardPileCount, Is.EqualTo(0));
            Assert.That(_collection.HandCount, Is.EqualTo(5));
            Assert.That(_collection.ExhaustPileCount, Is.EqualTo(initialDrawPileCount - 5));
        }
        
        for (int i = 0; i < _collection.HandCount; i++)
            _collection.ExhaustCardFromHandAtIndex(0);
        
        _collection.DrawNCards(10);
        
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_collection.DrawPileCount, Is.EqualTo(0));
            Assert.That(_collection.DiscardPileCount, Is.EqualTo(0));
            Assert.That(_collection.HandCount, Is.EqualTo(0));
            Assert.That(_collection.ExhaustPileCount, Is.EqualTo(initialDrawPileCount));
        }
    }
}