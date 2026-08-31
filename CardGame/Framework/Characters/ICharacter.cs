using CardGame.Constants;
using CardGame.Impl;

namespace CardGame.Framework.Characters;

public interface ICharacter
{
    public CharacterName Name { get; }
    
    public Deck Deck { get; }
    
    public int HandDrawCount { get; }
    
    public int GetResourceAmount(ResourceType resourceType);

    public int GetHealth();

    public void IncreaseResource(ResourceType resourceType, int amount);
    
    public void DecreaseResource(ResourceType resourceType, int amount);

    public void SpendResourcesForCard(ICard card);
    
    public bool CanPlayCard(ICard card);
    
    public void StartTurn();

    public void EndTurn();
}