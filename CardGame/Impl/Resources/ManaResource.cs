using CardGame.Constants;
using CardGame.Framework;

namespace CardGame.Impl.Resources;

public class ManaResource : IResource
{
    public ResourceType ResourceType =>  ResourceType.Mana;
    
    public int Amount { get; private set; }

    public ManaResource(int amount)
    {
        Amount = amount;
    }
    
    public void IncreaseBy(int amountIncreased)
    {
        if (amountIncreased < 0) 
            throw new ArgumentException($"Cannot increase mana by negative amount.");

        Amount += amountIncreased;
    }

    public void DecreaseBy(int amountDecreased)
    {
        if (amountDecreased < 0) 
            throw new ArgumentException($"Cannot decrease mana by negative amount.");
        
        if (amountDecreased > Amount) 
            throw new ArgumentException($"Cannot decrease mana by more than amount.");
        
        Amount -= amountDecreased;
    }

    public void StartTurn() { }

    public void EndTurn() { }
}