using CardGame.Constants;
using CardGame.Framework;

namespace CardGame.Impl.Resources;

public class EnergyResource : IResource
{
    public ResourceType ResourceType =>  ResourceType.Energy;
    
    public int Amount { get; private set; }
    
    public int Baseline { get; private set; }

    public EnergyResource(int baseline)
    {
        Baseline = baseline;
        Amount = baseline;
    }
    
    public void IncreaseBy(int amountIncreased)
    {
        if (amountIncreased < 0) 
            throw new ArgumentException($"Cannot increase energy by negative amount.");

        Amount += amountIncreased;
    }

    public void DecreaseBy(int amountDecreased)
    {
        if (amountDecreased < 0) 
            throw new ArgumentException($"Cannot decrease energy by negative amount.");
        
        int newAmount = Amount - amountDecreased;
        Amount = Math.Max(0, newAmount);
    }

    public void IncreaseBaseLineBy(int amountIncreased)
    {
        if (amountIncreased < 0) 
            throw new ArgumentException($"Cannot increase energy baseline by negative amount.");

        Baseline += amountIncreased;
    }

    public void DecreaseBaselineBy(int amountDecreased)
    {
        if (amountDecreased < 0) 
            throw new ArgumentException($"Cannot decrease energy baseline by negative amount.");

        Baseline -= amountDecreased;
    }

    public void StartTurn()
    {
        Amount = Baseline;
    }

    public void EndTurn() { }
}