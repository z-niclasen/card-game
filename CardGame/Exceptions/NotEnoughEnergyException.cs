namespace CardGame.Exceptions;

public class NotEnoughEnergyException : Exception
{
    public NotEnoughEnergyException(int currentEnergy, int energySpent) 
        : base($"Tried to spend {energySpent} with only {currentEnergy} remaining energy.") { }
}