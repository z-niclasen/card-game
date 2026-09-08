namespace CardGameCore.Exceptions;

public class CardNotInHandException : Exception
{
    public CardNotInHandException(string message) : base(message)
    {
        
    }
}