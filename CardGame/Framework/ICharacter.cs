using CardGame.Constants;
using CardGame.Impl;

namespace CardGame.Framework;

public interface ICharacter
{
    public CharacterName Name { get; }
    
    public Deck Deck { get; }
    
    public int HandDrawCount { get; }
    
    public int GetResource(ResourceType resourceType);
    
    public void SpendResource(ResourceType resourceType, int amount);
    
    public void GainResource(ResourceType resourceType, int amount);
    
    public void SpendResourcesForCard(ICard card);
    
    public bool CanPlayCard(ICard card);

    public void EndTurn();
}