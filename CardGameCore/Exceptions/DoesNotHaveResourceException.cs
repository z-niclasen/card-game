namespace CardGameCore.Exceptions;

public class DoesNotHaveResourceException : Exception
{
    public DoesNotHaveResourceException(string message) :  base(message)
    {
        
    }
}