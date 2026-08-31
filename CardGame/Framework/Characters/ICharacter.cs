using CardGame.Constants;
using CardGame.Impl;

namespace CardGame.Framework.Characters;

public interface ICharacter
{
    public CharacterName Name { get; }
    
    public ICharacterClass Class { get; } 
    
    public IList<Tag> Tags { get; }
    
    public int Health { get; }
    
    public int Energy { get; }
    
    public Deck Deck { get; }
    
    public int HandDrawCount { get; }
    
    public void AddTag(Tag tag);

    public IResource GetResource(ResourceType resourceType);
    
    public int GetResourceAmount(ResourceType resourceType);
    
    public bool HasResourceType(ResourceType resourceType);

    public void IncreaseResource(ResourceType resourceType, int amount);
    
    public void DecreaseResource(ResourceType resourceType, int amount);

    public void SpendResourcesForCard(ICard card);
    
    public bool CanPlayCard(ICard card);
    
    public void StartTurn();

    public void EndTurn();
}