using CardGame.Constants;
using CardGame.Framework;
using CardGame.Impl.Resources;

namespace CardGame.Test;

public class ResourceTest
{
    private HealthResource _health;
    private const int HealthMax = 10;
    
    private EnergyResource _energy;
    private const int  EnergyBaseline = 3;
    
    private ArmorResource _armor;
    private const int ArmorInitial = 2;
    
    private ManaResource _mana;
    private const int ManaInitial = 5;

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
    public void HealthAndEnergyHaveCorrectInitialAmounts()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_health.Amount, Is.EqualTo(HealthMax));
            Assert.That(_energy.Amount, Is.EqualTo(EnergyBaseline));
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
        
    }
}