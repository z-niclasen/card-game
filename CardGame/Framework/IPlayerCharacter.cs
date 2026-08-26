using CardGame.Constants;

namespace CardGame.Framework;

public interface IPlayerCharacter
{
    public PlayerClass Class { get; }
    
    public int HealthMax { get; }
    
    public int HealthCurrent { get; }
    
    public int EnergyMax { get; }
    
    public int EnergyCurrent { get; }
    
    public void DealDamage(int damage);

    public void GainHealth(int healthRegained);
    
    public void SpendEnergy(int energySpent);
    
    public void GainEnergy(int energyGained);

    public void ResetEnergy();
    
    public void IncreaseMaxHealth(int healthRegained);
    
    public void IncreaseMaxEnergy(int maxEnergyIncrease);
    
    public void DecreaseMaxHealth(int healthRegained);
    
    public void DecreaseMaxEnergy(int energyRegained);
}