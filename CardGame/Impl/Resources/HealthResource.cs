using CardGame.Framework;

namespace CardGame.Impl.Resources;

public class HealthResource : IResource
{
    public int Amount { get; private set; }
    
    public int Max { get; private set;  }

    public HealthResource(int max)
    {
        Max = max;
        Amount = Max;
    }
    
    public void IncreaseBy(int amountIncreased)
    {
        if (amountIncreased < 0) 
            throw new ArgumentException($"Cannot increase health by negative amount.");

        int newAmount = Amount + amountIncreased;
        Amount = Math.Min(Max, newAmount);
    }

    public void DecreaseBy(int amountDecreased)
    {
        if (amountDecreased < 0) 
            throw new ArgumentException($"Cannot decrease health by negative amount.");
        
        Amount -= amountDecreased;
    }
    
    public void IncreaseMaxBy(int amountIncreased)
    {
        if (amountIncreased < 0) 
            throw new ArgumentException($"Cannot increase max health by negative amount.");

        Max += amountIncreased;
    }

    public void DecreaseMaxBy(int amountDecreased)
    {
        if (amountDecreased < 0) 
            throw new ArgumentException($"Cannot decrease max health by negative amount.");
        
        Max -= amountDecreased;
        Amount = Math.Min(Max, Amount);
    }

    public void EndTurn() { }
}