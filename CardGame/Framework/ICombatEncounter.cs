namespace CardGame.Framework;

public interface ICombatEncounter
{
    public ICharacter Player { get; }
    
    public ICharacter Opponent { get; }
    
    public ICharacter InTurn { get; }

    public void PlayCardFromHandAtIndex(ICharacter player, int indexInHand);
    
    public void EndTurn(ICharacter player);
}