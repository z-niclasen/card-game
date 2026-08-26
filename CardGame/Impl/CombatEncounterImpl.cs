using CardGame.Exceptions;
using CardGame.Framework;

namespace CardGame.Impl;

public class CombatEncounterImpl : ICombatEncounter
{
    public ICharacter Player { get; }
    
    public ICharacter Opponent { get; }
    
    public ICharacter InTurn { get; private set; }
    
    private Dictionary<ICharacter, CombatCardCollection> CardsMap { get;  }

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
    
    public void PlayCardFromHandAtIndex(ICharacter player, int indexInHand)
    {
        throw new NotImplementedException();
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

    private ICharacter GetNextPlayer()
    {
        return Player == InTurn ? Opponent : Player;
    }
}