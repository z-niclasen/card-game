namespace CardGame.Framework;

public interface IResource
{
    public int Amount { get; }
    
    public void IncreaseBy(int amountIncreased);
    
    public void DecreaseBy(int amountDecreased);

    public void EndTurn();
}