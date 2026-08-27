using CardGame.Constants;
using CardGame.Exceptions;
using CardGame.Framework;
using CardGame.Framework.Effects;

namespace CardGame.Impl;

public class CombatEncounterImpl : ICombatEncounter
{
    public ICharacter Player { get; }
    
    public ICharacter Opponent { get; }
    
    public ICharacter InTurn { get; private set; }
    
    private Dictionary<ICharacter, CombatCardCollection> CardsMap { get; }

    public CombatEncounterImpl(ICharacter player, ICharacter opponent)
    {
        Player = player;
        Opponent = opponent;
        InTurn = Player;

        CardsMap = new Dictionary<ICharacter, CombatCardCollection>
        {
            { Player, new CombatCardCollection(player.Deck) },
            { Opponent, new CombatCardCollection(opponent.Deck) }
        };
    }
    
    public void PlayCardFromHandAtIndex(ICharacter source, int indexInHand, ICharacter target)
    {
        if (source != InTurn)
            throw new NotInTurnException($"Cannot play card as {source} is not in turn. Current player in turn: {InTurn}.");
        
        if (indexInHand < 0)
            throw new ArgumentException("IndexInHand cannot be negative.");
        
        CombatCardCollection cards = CardsMap[source];

        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(indexInHand, cards.HandCount);
        
        ICard card = cards.GetCardFromHandAtIndex(indexInHand);
        
        if (!source.CanPlayCard(card))
            throw new NotInTurnException($"Character {source} does not have resources to play card.");

        List<IEffectPrimitive> primitives = card.Effect.GetPrimitives(this, target, source);
        List<IEffectPrimitive> modifiedPrimitives = AdjustPrimitives(primitives, source, target);
        
        foreach (IEffectPrimitive primitive in primitives)
            primitive.Apply(this);
        
        cards.DiscardCardAtIndex(indexInHand);
        // TODO: Exhaust
    }

    private List<IEffectPrimitive> AdjustPrimitives(List<IEffectPrimitive> primitives, ICharacter source, ICharacter target)
    {
        return primitives;
    }

    public void EndTurn(ICharacter player)
    {
        if (player != InTurn)
            throw new NotInTurnException($"Cannot end turn as {player} is not in turn. Current player in turn: {InTurn}.");
        
        InTurn = GetNextPlayer();

        CombatCardCollection collection = CardsMap[InTurn];
        collection.DiscardHand();
        collection.DrawNCards(InTurn.HandDrawCount);
    }

    public void IncreaseResourceForCharacter(ICharacter character, ResourceType type, int amountGained)
    {
        character.IncreaseResource(type, amountGained);
    }

    public void DecreaseResourceForCharacter(ICharacter character, ResourceType type, int amountSpent)
    {
        character.DecreaseResource(type, amountSpent);
    }

    private ICharacter GetNextPlayer()
    {
        return Player == InTurn ? Opponent : Player;
    }
}