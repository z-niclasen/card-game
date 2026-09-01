using CardGame.Constants;

namespace CardGame.Framework.Characters;

public interface IAiCharacter :  ICharacter
{
    public void DoTurn(ICombatEncounter encounter);
}