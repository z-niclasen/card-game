using CardGame.Constants;
using CardGame.Framework;

namespace CardGame.Impl.Resources;

public class ArmorResource : IResource
{
    public ResourceType ResourceType =>  ResourceType.Armor;
    
    public int Amount { get; private set; }

    public ArmorResource(int amount)
    {
        Amount = amount;
    }
    
    public void IncreaseBy(int amountIncreased)
    {
        if (amountIncreased < 0) 
            throw new ArgumentException($"Cannot increase armor by negative amount.");

        Amount += amountIncreased;
    }

    public void DecreaseBy(int amountDecreased)
    {
        if (amountDecreased < 0) 
            throw new ArgumentException($"Cannot decrease armor by negative amount.");
        
        int newAmount = Amount - amountDecreased;
        Amount = Math.Max(newAmount, 0);
    }
    
    public void StartTurn() { }

    public void EndTurn() { }
}