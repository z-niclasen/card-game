namespace CardGameCore.Framework.Resources;

public interface IResourceMutable : IResource
{
    public void IncreaseBy(int amountIncreased);
    
    public void DecreaseBy(int amountDecreased);

    public void StartTurn();

    public void EndTurn();

    public void StartEncounter();

    public void EndEncounter();
}