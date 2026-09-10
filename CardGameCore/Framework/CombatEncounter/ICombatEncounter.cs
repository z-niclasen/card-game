using CardGameCore.Constants;
using CardGameCore.Framework.Characters;
using CardGameCore.Impl.CombatEncounter;

namespace CardGameCore.Framework.CombatEncounter;

public delegate void EndTurnDelegate(ICombatEncounter encounter);
public delegate void PlayCardDelegate(ICombatEncounter encounter, ICard playedCard, CombatTargetingContext ctx);
public delegate void EncounterFinishedDelegate(ICombatEncounter encounter);
public delegate void CharacterResourceChangeDelegate(ICombatEncounter encounter, ICharacter character, ResourceType type);

public interface ICombatEncounter
{
    public event EndTurnDelegate? OnEndTurn;
    public event PlayCardDelegate? OnPlayCard;
    public event EncounterFinishedDelegate? OnEncounterFinished;
    
    public event CharacterResourceChangeDelegate? OnCharacterResourceChange;
    
    public ICharacter Player { get; }
    
    public IAiCharacter Opponent { get; }
    
    public ICharacter InTurn { get; }
    
    public bool IsFinished { get; }
    
    public ICombatCardCollection GetCombatCardCollectionOfCharacter(ICharacter character);
    
    public int GetHandCountOfCharacter(ICharacter character);
    
    public int GetDrawPileCountOfCharacter(ICharacter character);
    
    public int GetDiscardPileCountOfCharacter(ICharacter character);
    
    public int GetExhaustPileCountOfCharacter(ICharacter character);

    public ICard GetCardFromHandAtIndex(ICharacter character, int index);

    public void PlayCardFromHand(ICharacter source, ICard cardToPlay, ICharacter target);
    
    public void EndTurn(ICharacter player);

    public void IncreaseResourceForCharacter(ICharacter character, ResourceType type, int amountGained);
    
    public void DecreaseResourceForCharacter(ICharacter character, ResourceType type, int amountSpent);
}