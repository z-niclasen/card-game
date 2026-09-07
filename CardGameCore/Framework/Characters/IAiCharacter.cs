namespace CardGameCore.Framework.Characters;

public interface IAiCharacter :  ICharacter
{
    public void DoTurn(ICombatEncounter encounter);
}