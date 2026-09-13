using CardGameCore.Constants;
using CardGameCore.Exceptions;
using CardGameCore.Framework;
using CardGameCore.Framework.Characters;
using CardGameCore.Framework.Relics;
using CardGameCore.Framework.Resources;
using CardGameCore.Impl.Relics;
using CardGameCore.Impl.Resources;

namespace CardGameCore.Impl;

public class CharacterImpl : ICharacter
{
    public event ResourceChangedDelegate? OnResourceChanged;
    
    public ICharacterClass Class { get; }

    public CharacterName Name => Class.Name;

    public IList<Tag> Tags { get; } 
    
    public int Health => GetResourceAmount(ResourceType.Health);
    
    public int Energy => GetResourceAmount(ResourceType.Energy);
    
    public Deck Deck { get; }

    public int HandDrawCount { get; }

    public RelicCollection RelicCollection { get; } = new();

    private Dictionary<ResourceType, IResourceMutable> Resources { get; }

    public CharacterImpl(ICharacterClass characterClass)
    {
        Class = characterClass;
        Deck = Class.StarterDeck;
        HandDrawCount = Class.InitialHandDrawCount;
        Resources = Class.InitialResources; // TODO: Clone?
        Tags = new List<Tag>(Class.InitialTags);
        RelicCollection.AddRelics(Class.InitialRelics);
    }

    public void AddTag(Tag tag)
    {
        Tags.Add(tag);
    }

    public void AddRelic(IRelic relic)
    {
        RelicCollection.AddRelic(relic);
    }

    public void RemoveRelic(IRelic relic)
    {
        RelicCollection.RemoveRelic(relic);
    }

    public IResource GetResource(ResourceType resourceType)
    {
        if (!Resources.TryGetValue(resourceType, out var resource))
            throw new DoesNotHaveResourceException($"Tried to access {resourceType} for {Name}, but it does not exist.");
        return resource;
    }
    
    public int GetResourceAmount(ResourceType resourceType)
    {
        return GetResource(resourceType).Amount;
    }
    
    bool ICharacter.HasResourceType(ResourceType resourceType)
    {
        return Resources.ContainsKey(resourceType);
    }

    public void DecreaseResource(ResourceType resourceType, int amount)
    {
        if (!Resources.TryGetValue(resourceType, out IResourceMutable? value))
            throw new DoesNotHaveResourceException($"Tried to spend {resourceType} for  {Name}, but it does not exist.");
        
        if (amount < 0)
            throw new ArgumentException($"Cannot spend negative amount of resource. ResourceType: {resourceType}.");
        
        value.DecreaseBy(amount);
        OnResourceChanged?.Invoke(this, resourceType);
    }

    public void IncreaseResource(ResourceType resourceType, int amount)
    {
        if (amount < 0)
            throw new ArgumentException($"Cannot gain negative amount of resource. ResourceType: {resourceType}.");

        if (Resources.TryGetValue(resourceType, out IResourceMutable? resource))
        {
            resource.IncreaseBy(amount);
            OnResourceChanged?.Invoke(this, resourceType);
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
        OnResourceChanged?.Invoke(this, resourceType);
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
        foreach (IResourceMutable resource in Resources.Values)
        {
            int previousAmount = resource.Amount;
            resource.StartTurn();
            int newAmount = resource.Amount;
            if (newAmount != previousAmount)
                OnResourceChanged?.Invoke(this, resource.ResourceType);
        }
    }
    
    public void EndTurn()
    {
        foreach (IResourceMutable resource in Resources.Values)
        {
            int previousAmount = resource.Amount;
            resource.EndTurn();
            int newAmount = resource.Amount;
            if (newAmount != previousAmount)
                OnResourceChanged?.Invoke(this, resource.ResourceType);
        }
    }

    private bool HasResourceType(ResourceType resourceType)
    {
        return Resources.ContainsKey(resourceType);
    }
}