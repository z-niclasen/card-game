using CardGame.Constants;
using CardGame.Framework.Characters;

namespace CardGame.Framework;

public interface ICombatEncounter
{
    public ICharacter Player { get; }
    
    public IAiCharacter Opponent { get; }
    
    public ICharacter InTurn { get; }
    
    public bool IsFinished { get; }
    
    public int GetHandCountOfCharacter(ICharacter character);
    
    public int GetDrawPileCountOfCharacter(ICharacter character);
    
    public int GetDiscardPileCountOfCharacter(ICharacter character);
    
    public int GetExhaustPileCountOfCharacter(ICharacter character);

    public void PlayCardFromHandAtIndex(ICharacter source, int indexInHand, ICharacter target);
    
    public void EndTurn(ICharacter player);

    public void IncreaseResourceForCharacter(ICharacter character, ResourceType type, int amountGained);
    
    public void DecreaseResourceForCharacter(ICharacter character, ResourceType type, int amountSpent);
}