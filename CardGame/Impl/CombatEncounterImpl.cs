using CardGame.Constants;
using CardGame.Exceptions;
using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Framework.Effects;

namespace CardGame.Impl;

public class CombatEncounterImpl : ICombatEncounter
{
    public ICharacter Player { get; }
    
    public ICharacter Opponent { get; }
    
    public ICharacter InTurn { get; private set; }

    public bool IsFinished { get; private set; } = false;
    
    private Dictionary<ICharacter, CombatCardCollection> CardsMap { get; }

    public CombatEncounterImpl(ICharacter player, ICharacter opponent)
    {
        Player = player;
        Opponent = opponent;
        InTurn = Player;

        CardsMap = new Dictionary<ICharacter, CombatCardCollection>
        {
            { Player, new CombatCardCollection(player.Deck, CombatCardCollection.ShuffleStrategy.Shuffle) },
            { Opponent, new CombatCardCollection(opponent.Deck, CombatCardCollection.ShuffleStrategy.NoShuffle) }
        };
        
        DiscardHandAndDrawNewForCharacter(InTurn);
    }

    public int GetHandCountOfCharacter(ICharacter character)
    {
        return CardsMap[character].HandCount;
    }

    public int GetDrawPileCountOfCharacter(ICharacter character)
    {
        return CardsMap[character].DrawPileCount;
    }

    public int GetDiscardPileCountOfCharacter(ICharacter character)
    {
        return CardsMap[character].DiscardPileCount;
    }

    public int GetExhaustPileCountOfCharacter(ICharacter character)
    {
        return CardsMap[character].ExhaustPileCount;
    }
    
    public void PlayCardFromHandAtIndex(ICharacter source, int indexInHand, ICharacter target)
    {
        if (IsFinished)
            throw new CombatEncounterInactiveException("Tried to play card, but combat encounter is inactive.");
        
        if (source != InTurn)
            throw new NotInTurnException($"Cannot play card as {source} is not in turn. Current player in turn: {InTurn}.");
        
        if (indexInHand < 0)
            throw new ArgumentException("IndexInHand cannot be negative.");
        
        CombatCardCollection cards = CardsMap[source];

        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(indexInHand, cards.HandCount);
        
        ICard card = cards.GetCardFromHandAtIndex(indexInHand);
        
        if (!source.CanPlayCard(card))
            throw new NotEnoughResourcesException($"Character {source} does not have resources to play card.");

        CombatTargetingContext ctx = new CombatTargetingContext(this, target, source);

        IEffect effect = card.Effect;
        IEffect adjustedEffect = AdjustEffect(effect, ctx);
        
        adjustedEffect.Apply(ctx);
        
        source.SpendResourcesForCard(card);
        cards.DiscardCardAtIndex(indexInHand);
        // TODO: Exhaust
    }

    private IEffect AdjustEffect(IEffect effect, CombatTargetingContext ctx)
    {
        IEnumerable<IEffectAdjustor> sourceAdjusters = ctx.Source.RelicCollection.Offensive;
        IEnumerable<IEffectAdjustor> targetAdjusters = ctx.Target.RelicCollection.Defensive;

        IEffect currentEffect = effect;
        
        foreach (IEffectAdjustor adjuster in sourceAdjusters)
            currentEffect = adjuster.Adjust(currentEffect, ctx);
        
        foreach (IEffectAdjustor adjuster in targetAdjusters)
            currentEffect = adjuster.Adjust(currentEffect, ctx);

        return currentEffect;
    }

    public void EndTurn(ICharacter player)
    {
        if (player != InTurn)
            throw new NotInTurnException($"Cannot end turn as {player} is not in turn. Current player in turn: {InTurn}.");
        
        CheckGameFinished();
        
        InTurn.EndTurn();
        InTurn = GetNextPlayer();
        InTurn.StartTurn();
        
        DiscardHandAndDrawNewForCharacter(InTurn);
    }

    public void IncreaseResourceForCharacter(ICharacter character, ResourceType type, int amountGained)
    {
        character.IncreaseResource(type, amountGained);
    }

    public void DecreaseResourceForCharacter(ICharacter character, ResourceType type, int amountSpent)
    {
        character.DecreaseResource(type, amountSpent);
    }

    private void DiscardHandAndDrawNewForCharacter(ICharacter character)
    {
        CombatCardCollection collection = CardsMap[character];
        collection.DiscardHand();
        collection.DrawNCards(character.HandDrawCount);
    }

    private void CheckGameFinished()
    {
        if (Player.Health <= 0 || Opponent.Health <= 0)
            IsFinished = true;
    }
    
    private ICharacter GetNextPlayer()
    {
        return Player == InTurn ? Opponent : Player;
    }
}