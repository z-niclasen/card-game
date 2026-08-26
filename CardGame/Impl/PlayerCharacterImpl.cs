using CardGame.Constants;
using CardGame.Exceptions;
using CardGame.Framework;

namespace CardGame.Impl;

public class PlayerCharacterImpl : IPlayerCharacter
{
    public PlayerClass Class { get; }
    
    public int HealthMax { get; private set;  }
    
    public int HealthCurrent { get; private set; }
    
    public int EnergyMax { get; private set; }
    
    public int EnergyCurrent { get; private set; }
    
    public PlayerCharacterImpl(PlayerClass playerClass,  int healthMax, int energyMax)
    {
        Class = playerClass;
        HealthMax = healthMax;
        HealthCurrent = healthMax;
        EnergyMax = energyMax;
        EnergyCurrent = energyMax;
    }
    
    public void DealDamage(int damage)
    {
        if (damage <= 0) 
            return;

        HealthCurrent -= damage;
    }

    public void GainHealth(int healthRegained)
    {
        if (healthRegained <= 0)
            return;
        
        int newHealth = HealthCurrent + healthRegained;
        HealthCurrent = Math.Min(newHealth, HealthMax);
    }

    public void SpendEnergy(int energySpent)
    {
        if (energySpent <= 0)
            return;
        
        if (energySpent > EnergyCurrent)
            throw new NotEnoughEnergyException(EnergyCurrent, energySpent);
        
        EnergyCurrent -= energySpent;
    }

    public void GainEnergy(int energyGained)
    {
        if (energyGained <= 0)
            return;
        
        int newEnergy = EnergyCurrent + energyGained;
        EnergyCurrent = Math.Min(newEnergy, EnergyMax);
    }

    public void ResetEnergy()
    {
        EnergyCurrent = EnergyMax;
    }

    public void IncreaseMaxHealth(int maxHealthIncrease)
    {
        if (maxHealthIncrease <= 0)
            return;

        HealthMax += maxHealthIncrease;
    }

    public void IncreaseMaxEnergy(int maxEnergyIncrease)
    {
        if (maxEnergyIncrease <= 0)
            return;

        EnergyMax += maxEnergyIncrease;
    }

    public void DecreaseMaxHealth(int maxHealthDecrease)
    {
        if (maxHealthDecrease <= 0)
            return;

        int newMaxHealth = HealthMax - maxHealthDecrease;
        HealthMax = Math.Max(newMaxHealth, 1);
        HealthCurrent = Math.Min(HealthMax, HealthCurrent);
    }

    public void DecreaseMaxEnergy(int maxEnergyDecrease)
    {
        if (maxEnergyDecrease <= 0)
            return;

        int newMaxEnergy = EnergyMax - maxEnergyDecrease;
        EnergyMax = Math.Max(maxEnergyDecrease, 1);
    }
}