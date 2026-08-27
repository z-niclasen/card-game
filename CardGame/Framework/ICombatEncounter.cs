using CardGame.Constants;

namespace CardGame.Framework;

public interface ICombatEncounter
{
    public ICharacter Player { get; }
    
    public ICharacter Opponent { get; }
    
    public ICharacter InTurn { get; }

    public void PlayCardFromHandAtIndex(ICharacter source, int indexInHand, ICharacter target);
    
    public void EndTurn(ICharacter player);

    public void IncreaseResourceForCharacter(ICharacter character, ResourceType type, int amountGained);
    
    public void DecreaseResourceForCharacter(ICharacter character, ResourceType type, int amountSpent);
}