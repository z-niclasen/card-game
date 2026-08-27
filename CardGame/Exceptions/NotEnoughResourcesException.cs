namespace CardGame.Exceptions;

public class NotEnoughResourcesException : Exception
{
    public NotEnoughResourcesException(string message) : base(message)
    {
        
    }
}