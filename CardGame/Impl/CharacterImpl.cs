using CardGame.Constants;
using CardGame.Exceptions;
using CardGame.Framework;

namespace CardGame.Impl;

public class CharacterImpl : ICharacter
{
    public ICharacterClass Class { get; }
    public CharacterName Name { get; }
    
    public Deck Deck { get; }

    public int HandDrawCount => 5; // TODO: Ask class.
    
    private Dictionary<ResourceType, int> Resources { get; } = new();
    public int GetResource(ResourceType resourceType)
    {
        if (!Resources.TryGetValue(resourceType, out var resource))
            throw new DoesNotHaveResourceException($"Tried to access {resourceType} for  {Name}, but it does not exist.");
        
        return resource;
    }

    public void SpendResource(ResourceType resourceType, int amount)
    {
        if (!Resources.ContainsKey(resourceType))
            throw new DoesNotHaveResourceException($"Tried to spend {resourceType} for  {Name}, but it does not exist.");
        
        if (amount < 0)
            throw new ArgumentException($"Cannot spend negative amount of resource. ResourceType: {resourceType}.");

        Resources[resourceType] -= amount;
    }

    public void GainResource(ResourceType resourceType, int amount)
    {
        if (amount < 0)
            throw new ArgumentException($"Cannot gain negative amount of resource. ResourceType: {resourceType}.");
        
        Resources[resourceType] = Resources.GetValueOrDefault(resourceType, 0) + amount;
    }
    
    public CharacterImpl()
    {
        
    }

    public bool CanPlayCard(ICard card)
    {
        foreach (var (resourceType, resourceCost) in card.Cost)
        {
            if (!Resources.ContainsKey(resourceType))
                throw new DoesNotHaveResourceException($"Tried to play costing {resourceType} for {Name}, but it does not exist.");

            if (GetResource(resourceType) < resourceCost)
                return false;
        }

        return true;
    }

    public void EndTurn()
    {
        
    }

    public void SpendResourcesForCard(ICard card)
    {
        throw new NotImplementedException();
    }
}