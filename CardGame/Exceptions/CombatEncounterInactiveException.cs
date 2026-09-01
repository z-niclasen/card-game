namespace CardGame.Exceptions;

public class CombatEncounterInactiveException : Exception
{
    public CombatEncounterInactiveException(string message) :  base(message)
    {
        
    }
}