using CardGame.Constants;
using CardGame.Framework;
using CardGame.Impl.Resources;

namespace CardGame.Test;

public class ResourceTest
{
    private HealthResource _health;
    private const int HealthMax = 10;
    
    private EnergyResource _energy;
    private const int  EnergyBaseline = 4;
    
    private ArmorResource _armor;
    private const int ArmorInitial = 2;
    
    private ManaResource _mana;
    private const int ManaInitial = 6;

    private Dictionary<ResourceType, IResource> _resources;

    [SetUp]
    public void Setup()
    {
        _health = new HealthResource(HealthMax);
        _energy = new EnergyResource(EnergyBaseline);
        _armor = new ArmorResource(ArmorInitial);
        _mana = new ManaResource(ManaInitial);

        _resources = new Dictionary<ResourceType, IResource>
        {
            { ResourceType.Health, _health },
            { ResourceType.Energy, _energy },
            { ResourceType.Armor, _armor },
            { ResourceType.Mana, _mana },
        };
    }

    [Test]
    public void ResourcesAreCorrectlyInitialized()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_health.Amount, Is.EqualTo(HealthMax));
            Assert.That(_energy.Amount, Is.EqualTo(EnergyBaseline));
            
            Assert.That(_health.ResourceType, Is.EqualTo(ResourceType.Health));
            Assert.That(_energy.ResourceType, Is.EqualTo(ResourceType.Energy));
            Assert.That(_armor.ResourceType, Is.EqualTo(ResourceType.Armor));
            Assert.That(_mana.ResourceType, Is.EqualTo(ResourceType.Mana));
        }
    }

    [TestCase(ResourceType.Health)]
    public void DecreasesResourceWithoutMinimumCorrectly(ResourceType resourceType)
    {
        IResource resource = _resources[resourceType];
        
        Assert.That(resource.Amount, Is.GreaterThan(0));
        
        int decreaseAmount = resource.Amount;
        resource.DecreaseBy(decreaseAmount);
        
        Assert.That(resource.Amount, Is.EqualTo(0));
        
        resource.DecreaseBy(decreaseAmount);
        
        Assert.That(resource.Amount, Is.EqualTo(-decreaseAmount));
    }
    
    [TestCase(ResourceType.Energy)]
    [TestCase(ResourceType.Armor)]
    [TestCase(ResourceType.Mana)]
    public void DecreasesResourceWithMinimumCorrectly(ResourceType resourceType)
    {
        IResource resource = _resources[resourceType];
        
        Assert.That(resource.Amount, Is.GreaterThan(0));
        
        int decreaseAmount = resource.Amount;
        resource.DecreaseBy(decreaseAmount);
        
        Assert.That(resource.Amount, Is.EqualTo(0));
        
        resource.DecreaseBy(decreaseAmount);
        
        Assert.That(resource.Amount, Is.EqualTo(0));
    }

    [TestCase(ResourceType.Health)]
    public void IncreasesResourceWithMaximumCorrectly(ResourceType resourceType)
    {
        IResource resource = _resources[resourceType];
        int initialAmount = resource.Amount;

        int changeAmount = initialAmount / 2;
        resource.DecreaseBy(changeAmount);
        
        Assert.That(resource.Amount, Is.EqualTo(initialAmount - changeAmount));
        
        resource.IncreaseBy(changeAmount);
        
        Assert.That(resource.Amount, Is.EqualTo(initialAmount));
        
        resource.IncreaseBy(changeAmount);
        
        Assert.That(resource.Amount, Is.EqualTo(initialAmount));
    }
    
    [TestCase(ResourceType.Energy)]
    [TestCase(ResourceType.Armor)]
    [TestCase(ResourceType.Mana)]
    public void IncreasesResourceWithoutMaximumCorrectly(ResourceType resourceType)
    {
        IResource resource = _resources[resourceType];
        int initialAmount = resource.Amount;

        int changeAmount = initialAmount / 2;
        resource.DecreaseBy(changeAmount);
        
        Assert.That(resource.Amount, Is.EqualTo(initialAmount - changeAmount));
        
        resource.IncreaseBy(changeAmount);
        
        Assert.That(resource.Amount, Is.EqualTo(initialAmount));
        
        resource.IncreaseBy(changeAmount);
        
        Assert.That(resource.Amount, Is.EqualTo(initialAmount + changeAmount));
    }

    [Test]
    public void IncreasesHealthMaximumCorrectly()
    {
        int initialHealthMax = _health.Max;
        const int maxHealthIncrease = 2;
        
        _health.IncreaseMaxBy(maxHealthIncrease);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(_health.Amount, Is.EqualTo(initialHealthMax));
            Assert.That(_health.Max, Is.EqualTo(initialHealthMax + maxHealthIncrease));
        }
    }
    
    [Test]
    public void DecreasesHealthMaximumCorrectly()
    {
        int initialHealthMax = _health.Max;
        const int maxHealthDecrease = 2;
        
        _health.DecreaseMaxBy(maxHealthDecrease);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(_health.Amount, Is.EqualTo(initialHealthMax - maxHealthDecrease));
            Assert.That(_health.Max, Is.EqualTo(initialHealthMax - maxHealthDecrease));
        }
    }

    [Test]
    public void IncreasesEnergyBaselineCorrectly()
    {
        int initialBaseline = _energy.Baseline;
        const int baselineIncrease = 2;
        
        _energy.IncreaseBaselineBy(baselineIncrease);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(_energy.Amount, Is.EqualTo(initialBaseline));
            Assert.That(_energy.Baseline, Is.EqualTo(initialBaseline + baselineIncrease));
        }
    }
    
    [Test]
    public void DecreasesEnergyBaselineCorrectly()
    {
        int initialBaseline = _energy.Baseline;
        const int baselineDecrease = 2;
        
        _energy.DecreaseBaselineBy(baselineDecrease);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(_energy.Amount, Is.EqualTo(initialBaseline));
            Assert.That(_energy.Baseline, Is.EqualTo(initialBaseline - baselineDecrease));
        }
    }

    [Test]
    public void ReplenishesEnergyOnTurnStartAndEndCorrectly()
    {
        const int changeAmount = 2;
        
        _energy.DecreaseBy(changeAmount);
        Assert.That(_energy.Amount, Is.EqualTo(EnergyBaseline - changeAmount));
        
        _energy.EndTurn();
        Assert.That(_energy.Amount, Is.EqualTo(EnergyBaseline - changeAmount));
        
        _energy.StartTurn();
        Assert.That(_energy.Amount, Is.EqualTo(EnergyBaseline));
        
        _energy.IncreaseBy(changeAmount);
        Assert.That(_energy.Amount, Is.EqualTo(EnergyBaseline + changeAmount));
        
        _energy.EndTurn();
        Assert.That(_energy.Amount, Is.EqualTo(EnergyBaseline + changeAmount));
        
        _energy.StartTurn();
        Assert.That(_energy.Amount, Is.EqualTo(EnergyBaseline));
    }

    [TestCase(ResourceType.Health)]
    [TestCase(ResourceType.Armor)]
    [TestCase(ResourceType.Mana)]
    public void ResourcesUnchangedByTurnStartAndEndAreUnchanged(ResourceType resourceType)
    {
        IResource resource = _resources[resourceType];
        int changeAmount = resource.Amount / 2;
        
        resource.DecreaseBy(changeAmount);
        int amountAfterDecrease = resource.Amount;
        Assert.That(resource.Amount, Is.EqualTo(amountAfterDecrease));
        
        resource.EndTurn();
        Assert.That(resource.Amount, Is.EqualTo(amountAfterDecrease));
        
        resource.StartTurn();
        Assert.That(resource.Amount, Is.EqualTo(amountAfterDecrease));
        
        resource.IncreaseBy(2 * changeAmount);
        int amountAfterIncrease = resource.Amount;
        Assert.That(resource.Amount, Is.EqualTo(amountAfterIncrease));
        
        resource.EndTurn();
        Assert.That(resource.Amount, Is.EqualTo(amountAfterIncrease));
        
        resource.StartTurn();
        Assert.That(resource.Amount, Is.EqualTo(amountAfterIncrease));
    }

    [Test]
    public void DoesNotChangeHealthOnEncounterStartAndEnd()
    {
        _health.DecreaseBy(1);
        int amount = _health.Amount;
        
        _health.StartEncounter();
        Assert.That(_health.Amount, Is.EqualTo(amount));
        
        _health.EndEncounter();
        Assert.That(_health.Amount, Is.EqualTo(amount));
    }
    
    [Test]
    public void ReplenishesEnergyOnEncounterStartAndEndCorrectly()
    {
        const int changeAmount = 2;
        
        _energy.DecreaseBy(changeAmount);
        _energy.StartEncounter();
        Assert.That(_energy.Amount, Is.EqualTo(EnergyBaseline));
        
        _energy.DecreaseBy(changeAmount);
        _energy.EndEncounter();
        Assert.That(_energy.Amount, Is.EqualTo(EnergyBaseline));
        
        _energy.IncreaseBy(changeAmount);
        _energy.StartEncounter();
        Assert.That(_energy.Amount, Is.EqualTo(EnergyBaseline));
        
        _energy.IncreaseBy(changeAmount);
        _energy.EndEncounter();
        Assert.That(_energy.Amount, Is.EqualTo(EnergyBaseline));
    }
    
    [Test]
    public void ResetsArmorOnEncounterStartAndEndCorrectly()
    {
        const int changeAmount = 2;
        
        _armor.StartEncounter();
        Assert.That(_armor.Amount, Is.EqualTo(0));
        
        _armor.IncreaseBy(changeAmount);
        _armor.EndEncounter();
        Assert.That(_armor.Amount, Is.EqualTo(0));
    }

    [Test]
    public void ChangesManaOnEncounterStartAndEndCorrectly()
    {
        _mana.StartEncounter();
        Assert.That(_mana.Amount, Is.EqualTo(ManaInitial));
        
        _mana.EndEncounter();
        Assert.That(_mana.Amount, Is.EqualTo(ManaInitial + _mana.ManaGainOnEncounterEnd));
    }

    [TestCase(ResourceType.Health)]
    [TestCase(ResourceType.Energy)]
    [TestCase(ResourceType.Armor)]
    [TestCase(ResourceType.Mana)]
    public void CannotChangeResourceByNegativeAmount(ResourceType resourceType)
    {
        IResource resource =  _resources[resourceType];
        
        Assert.Throws<ArgumentException>(() => resource.IncreaseBy(-1));
        Assert.Throws<ArgumentException>(() => resource.DecreaseBy(-1));
    }

    [Test]
    public void CannotChangeHealthMaximumByNegativeAmount()
    {
        Assert.Throws<ArgumentException>(() => _health.IncreaseMaxBy(-1));
        Assert.Throws<ArgumentException>(() => _health.DecreaseMaxBy(-1));
    }
    
    [Test]
    public void CannotChangeEnergyBaselineByNegativeAmount()
    {
        Assert.Throws<ArgumentException>(() => _energy.IncreaseBaselineBy(-1));
        Assert.Throws<ArgumentException>(() => _energy.DecreaseBaselineBy(-1));
    }
}