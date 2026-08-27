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
    
    private Dictionary<ResourceType, IResource> Resources { get; } = new();
    
    public int GetResourceAmount(ResourceType resourceType)
    {
        if (!Resources.TryGetValue(resourceType, out var resource))
            throw new DoesNotHaveResourceException($"Tried to access {resourceType} for {Name}, but it does not exist.");
        
        return resource.Amount;
    }

    public void SpendResource(ResourceType resourceType, int amount)
    {
        if (!Resources.TryGetValue(resourceType, out IResource? value))
            throw new DoesNotHaveResourceException($"Tried to spend {resourceType} for  {Name}, but it does not exist.");
        
        if (amount < 0)
            throw new ArgumentException($"Cannot spend negative amount of resource. ResourceType: {resourceType}.");
        
        value.DecreaseBy(amount);
    }

    public void GainResource(ResourceType resourceType, int amount)
    {
        if (!HasResourceType(resourceType))
            throw new DoesNotHaveResourceException(
                $"Tried to gain {resourceType} resource on character {Name}, but that resource does not exist on the character.");
        
        if (amount < 0)
            throw new ArgumentException($"Cannot gain negative amount of resource. ResourceType: {resourceType}.");
        
        Resources[resourceType].IncreaseBy(amount);
    }

    public void AddResourceType(IResource resource)
    {
        ResourceType resourceType = resource.ResourceType;
        if (HasResourceType(resourceType))
            throw new ArgumentException(
                $"Tried to add resource type {resourceType} to character {Name}, but it already has that resource type.");
        
        Resources.Add(resourceType, resource);
    }
    
    public CharacterImpl()
    {
        
    }

    public bool CanPlayCard(ICard card)
    {
        foreach (var (resourceType, resourceCost) in card.Cost)
        {
            if (!HasResourceType(resourceType))
                throw new DoesNotHaveResourceException(
                    $"Checked whether character {Name} can play a card costing {resourceType}, but that resource does not exist on the character.");

            if (GetResourceAmount(resourceType) < resourceCost)
                return false;
        }

        return true;
    }

    public void SpendResourcesForCard(ICard card)
    {
        if (!CanPlayCard(card))
            throw new DoesNotHaveResourceException($"Tried to play card with insufficient resources.");

        foreach (var (resourceType, resourceCost) in card.Cost)
            SpendResource(resourceType, resourceCost);
    }

    public void StartTurn()
    {
        foreach (IResource resource in Resources.Values)
            resource.StartTurn();
    }
    
    public void EndTurn()
    {
        foreach (IResource resource in Resources.Values)
            resource.EndTurn();
    }

    private bool HasResourceType(ResourceType resourceType)
    {
        return Resources.ContainsKey(resourceType);
    }
}