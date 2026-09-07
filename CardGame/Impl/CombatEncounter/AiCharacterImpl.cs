using System.Security.Principal;
using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Impl.Relics;

namespace CardGame.Impl;

public class AiCharacterImpl(IAiCharacterClass aiCharacterClass) : IAiCharacter
{
    
    private readonly ICharacter _characterImplementation = new CharacterImpl(aiCharacterClass);
    
    private readonly AiStrategy _aiStrategy = aiCharacterClass.Strategy;

    public CharacterName Name => _characterImplementation.Name;

    public ICharacterClass Class => _characterImplementation.Class;

    public IList<Tag> Tags => _characterImplementation.Tags;

    public int Health => _characterImplementation.Health;

    public int Energy => _characterImplementation.Energy;

    public Deck Deck => _characterImplementation.Deck;

    public int HandDrawCount => _characterImplementation.HandDrawCount;

    public RelicCollection RelicCollection => _characterImplementation.RelicCollection;

    public void AddTag(Tag tag)
    {
        _characterImplementation.AddTag(tag);
    }

    public void AddRelic(IRelic relic)
    {
        _characterImplementation.AddRelic(relic);
    }

    public void RemoveRelic(IRelic relic)
    {
        _characterImplementation.RemoveRelic(relic);
    }

    public IResource GetResource(ResourceType resourceType)
    {
        return _characterImplementation.GetResource(resourceType);
    }

    public int GetResourceAmount(ResourceType resourceType)
    {
        return _characterImplementation.GetResourceAmount(resourceType);
    }

    public bool HasResourceType(ResourceType resourceType)
    {
        return _characterImplementation.HasResourceType(resourceType);
    }

    public void IncreaseResource(ResourceType resourceType, int amount)
    {
        _characterImplementation.IncreaseResource(resourceType, amount);
    }

    public void DecreaseResource(ResourceType resourceType, int amount)
    {
        _characterImplementation.DecreaseResource(resourceType, amount);
    }

    public void SpendResourcesForCard(ICard card)
    {
        _characterImplementation.SpendResourcesForCard(card);
    }

    public bool CanPlayCard(ICard card)
    {
        return _characterImplementation.CanPlayCard(card);
    }

    public void StartTurn()
    {
        _characterImplementation.StartTurn();
    }

    public void EndTurn()
    {
        _characterImplementation.EndTurn();
    }

    public void DoTurn(ICombatEncounter encounter)
    {
        switch (_aiStrategy)
        {
            case AiStrategy.DoNothing:
                break;
            case AiStrategy.PlayZero:
                encounter.PlayCardFromHandAtIndex(this, 0, encounter.Player);
                break;
        }
    }
}