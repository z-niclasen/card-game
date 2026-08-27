using CardGame.Constants;

namespace CardGame.Framework;

public interface IResource
{
    public ResourceType ResourceType { get; }
    
    public int Amount { get; }
    
    public void IncreaseBy(int amountIncreased);
    
    public void DecreaseBy(int amountDecreased);

    public void StartTurn();

    public void EndTurn();
}