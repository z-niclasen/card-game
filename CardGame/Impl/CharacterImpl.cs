using CardGame.Constants;
using CardGame.Exceptions;
using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Impl.Resources;

namespace CardGame.Impl;

public class CharacterImpl : ICharacter
{
    public ICharacterClass Class { get; }
    public CharacterName Name => Class.Name;
    
    public int Health => GetResourceAmount(ResourceType.Health);
    
    public int Energy => GetResourceAmount(ResourceType.Energy);
    
    public Deck Deck { get; }

    public int HandDrawCount { get; }
    
    private Dictionary<ResourceType, IResource> Resources { get; }
    
    public CharacterImpl(ICharacterClass characterClass)
    {
        Class = characterClass;
        Deck = Class.StarterDeck;
        HandDrawCount = Class.InitialHandDrawCount;
        Resources = Class.InitialResources;
    }
    
    public int GetResourceAmount(ResourceType resourceType)
    {
        if (!Resources.TryGetValue(resourceType, out var resource))
            throw new DoesNotHaveResourceException($"Tried to access {resourceType} for {Name}, but it does not exist.");
        
        return resource.Amount;
    }

    bool ICharacter.HasResourceType(ResourceType resourceType)
    {
        return Resources.ContainsKey(resourceType);
    }

    public void DecreaseResource(ResourceType resourceType, int amount)
    {
        if (!Resources.TryGetValue(resourceType, out IResource? value))
            throw new DoesNotHaveResourceException($"Tried to spend {resourceType} for  {Name}, but it does not exist.");
        
        if (amount < 0)
            throw new ArgumentException($"Cannot spend negative amount of resource. ResourceType: {resourceType}.");
        
        value.DecreaseBy(amount);
    }

    public void IncreaseResource(ResourceType resourceType, int amount)
    {
        if (amount < 0)
            throw new ArgumentException($"Cannot gain negative amount of resource. ResourceType: {resourceType}.");

        if (Resources.TryGetValue(resourceType, out IResource? resource))
        {
            resource.IncreaseBy(amount);
            return;
        }

        switch (resourceType)
        {
            case ResourceType.Armor:
                Resources.Add(resourceType, new ArmorResource(amount));
                break;
            case ResourceType.Mana:
                Resources.Add(resourceType, new ManaResource(amount));
                break;
            case ResourceType.Health:
            case ResourceType.Energy:
                throw new InvalidOperationException(
                    $"Tried to add resource type {resourceType} to character {Name}, but that character should already have said resource type.");
            default:
                throw new ArgumentOutOfRangeException(nameof(resourceType), resourceType, null);
        }
    }

    public void AddResourceType(IResource resource)
    {
        ResourceType resourceType = resource.ResourceType;
        if (HasResourceType(resourceType))
            throw new ArgumentException(
                $"Tried to add resource type {resourceType} to character {Name}, but it already has that resource type.");
        
        Resources.Add(resourceType, resource);
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
            DecreaseResource(resourceType, resourceCost);
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