using CardGameCore.Constants;
using CardGameCore.Impl;
using CardGameCore.Impl.Relics;

namespace CardGameCore.Framework.Characters;

public delegate void DecreaseResourceDelegate(ICharacter character, ResourceType type, int amount);
public delegate void IncreaseResourceDelegate(ICharacter character, ResourceType type, int amount);

public interface ICharacter
{
    public event DecreaseResourceDelegate OnDecreaseResource;

    public event IncreaseResourceDelegate OnIncreaseResource;
    
    public CharacterName Name { get; }
    
    public ICharacterClass Class { get; } 
    
    public IList<Tag> Tags { get; }
    
    public int Health { get; }
    
    public int Energy { get; }
    
    public Deck Deck { get; }
    
    public int HandDrawCount { get; }
    
    public RelicCollection RelicCollection { get; }

    public void AddTag(Tag tag);
    
    public void AddRelic(IRelic relic);
    
    public void RemoveRelic(IRelic relic);

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