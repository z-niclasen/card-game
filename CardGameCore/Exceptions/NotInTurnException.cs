namespace CardGameCore.Exceptions;

public class NotInTurnException : Exception
{
    public NotInTurnException(string message) : base(message)
    {
        
    }
}