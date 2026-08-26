using CardGame.Framework;

namespace CardGame.Impl.Resources;

public class EnergyResource : IResource
{
    public int Amount { get; private set; }
    
    public int Baseline { get; }

    public EnergyResource(int baseline)
    {
        Baseline = baseline;
        Amount = baseline;
    }
    
    public void IncreaseBy(int amountIncreased)
    {
        if (amountIncreased < 0) 
            throw new ArgumentException($"Cannot gain negative amount of health.");

        int newAmount = Amount + amountIncreased;
        Amount = Math.Min(Max, newAmount);
    }

    public void DecreaseBy(int amountDecreased)
    {
        if (amountDecreased < 0) 
            throw new ArgumentException($"Cannot decrease energy by negative amount.");
        
        if (amountDecreased > Amount) 
            throw new ArgumentException($"Cannot decrease energy by more than amount.");
        
        Amount -= amountDecreased;
    }

    public void EndTurn()
    {
        Amount = Baseline;
    }
}