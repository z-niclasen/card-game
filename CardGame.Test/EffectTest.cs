using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Framework.Effects;
using CardGame.Impl;
using CardGame.Impl.Effects;
using CardGame.Library.Characters.EnemyCharacters;
using CardGame.Library.Characters.PlayerCharacters;

namespace CardGame.Test;

public class EffectTest
{
    private ICharacter _steve;
    private ICharacter _slime;
    private ICombatEncounter _encounter;
    private CombatTargetingContext _ctx;

    [SetUp]
    public void Setup()
    {
        ICharacterClass steveClass = new SteveClass();
        _steve = new CharacterImpl(steveClass);

        ICharacterClass greenSlimeClass = new GreenSlimeClass();
        _slime = new CharacterImpl(greenSlimeClass);

        _encounter = new CombatEncounterImpl(_steve, _slime);
        
        _ctx = new CombatTargetingContext(_encounter, _slime, _steve);
    }

    [Test]
    public void AppliesDecreaseHealthPrimitiveCorrectly()
    {
        int slimeStartingHealth = _slime.Health;
        int damageAmount = slimeStartingHealth - 1;

        IEffectPrimitive damagePrimitive = new DecreaseResourcePrimitive(ResourceType.Health, damageAmount);
        damagePrimitive.Apply(_ctx);

        Assert.That(_slime.Health, Is.EqualTo(slimeStartingHealth - damageAmount));

        IEffectPrimitive damageAllHealthPrimitive =
            new DecreaseResourcePrimitive(ResourceType.Health, slimeStartingHealth);
        damageAllHealthPrimitive.Apply(_ctx);

        Assert.That(_slime.Health, Is.EqualTo(-damageAmount));
    }

    [Test]
    public void AppliesDecreaseEnergyPrimitiveCorrectly()
    {
        int slimeStartingEnergy = _slime.Energy;

        IEffectPrimitive primitive = new DecreaseResourcePrimitive(ResourceType.Energy, slimeStartingEnergy);
        primitive.Apply(_ctx);

        Assert.That(_slime.Energy, Is.EqualTo(0));

        primitive.Apply(_ctx);

        Assert.That(_slime.Energy, Is.EqualTo(0));
    }

    [Test]
    public void AppliesDecreaseArmorPrimitiveCorrectly()
    {
        const ResourceType armorType = ResourceType.Armor;
        const int changeAmount = 2;

        IEffectPrimitive increasePrimitive = new IncreaseResourcePrimitive(armorType, changeAmount);
        increasePrimitive.Apply(_ctx);

        Assert.That(_slime.GetResourceAmount(armorType), Is.EqualTo(changeAmount));

        IEffectPrimitive decreasePrimitive = new DecreaseResourcePrimitive(armorType, changeAmount);
        decreasePrimitive.Apply(_ctx);

        Assert.That(_slime.GetResourceAmount(armorType), Is.EqualTo(0));

        decreasePrimitive.Apply(_ctx);

        Assert.That(_slime.GetResourceAmount(armorType), Is.EqualTo(0));
    }

    [Test]
    public void AppliesDecreaseManaPrimitiveCorrectly()
    {
        const ResourceType manaType = ResourceType.Mana;
        const int changeAmount = 2;

        IEffectPrimitive increasePrimitive = new IncreaseResourcePrimitive(manaType, changeAmount);
        increasePrimitive.Apply(_ctx);

        Assert.That(_slime.GetResourceAmount(manaType), Is.EqualTo(changeAmount));

        IEffectPrimitive decreasePrimitive = new DecreaseResourcePrimitive(manaType, changeAmount);
        decreasePrimitive.Apply(_ctx);

        Assert.That(_slime.GetResourceAmount(manaType), Is.EqualTo(0));

        decreasePrimitive.Apply(_ctx);

        Assert.That(_slime.GetResourceAmount(manaType), Is.EqualTo(0));
    }

    [Test]
    public void AppliesIncreaseHealthPrimitiveCorrectly()
    {
        const ResourceType healthType = ResourceType.Health;
        int slimeStartingHealth = _slime.Health;
        const int healthChange = 3;

        IEffectPrimitive damagePrimitive = new DecreaseResourcePrimitive(healthType, healthChange);
        damagePrimitive.Apply(_ctx);

        Assert.That(_slime.Health, Is.EqualTo(slimeStartingHealth - healthChange));

        IEffectPrimitive healPrimitive = new IncreaseResourcePrimitive(healthType, healthChange);
        healPrimitive.Apply(_ctx);

        Assert.That(_slime.Health, Is.EqualTo(slimeStartingHealth));

        healPrimitive.Apply(_ctx);

        Assert.That(_slime.Health, Is.EqualTo(slimeStartingHealth));
    }

    [Test]
    public void AppliesIncreaseEnergyPrimitiveCorrectly()
    {
        const ResourceType energyType = ResourceType.Energy;
        int slimeStartingEnergy = _slime.Energy;
        const int energyChange = 1;

        IEffectPrimitive damagePrimitive = new DecreaseResourcePrimitive(energyType, energyChange);
        damagePrimitive.Apply(_ctx);

        Assert.That(_slime.Energy, Is.EqualTo(slimeStartingEnergy - energyChange));

        IEffectPrimitive healPrimitive = new IncreaseResourcePrimitive(energyType, energyChange);
        healPrimitive.Apply(_ctx);

        Assert.That(_slime.Energy, Is.EqualTo(slimeStartingEnergy));

        healPrimitive.Apply(_ctx);

        Assert.That(_slime.Energy, Is.EqualTo(slimeStartingEnergy + energyChange));
    }

    [TestCase(ResourceType.Armor)]
    [TestCase(ResourceType.Mana)]
    public void AppliesIncreaseResourcePrimitiveCorrectlyForResourceTypeCharacterDoesNotHave(ResourceType resourceType)
    {
        const int resourceIncrease = 2;

        Assert.That(_slime.HasResourceType(resourceType), Is.False);

        IEffectPrimitive primitive = new IncreaseResourcePrimitive(resourceType, resourceIncrease);
        primitive.Apply(_ctx);

        Assert.That(_slime.HasResourceType(resourceType), Is.True);
        Assert.That(_slime.GetResourceAmount(resourceType), Is.EqualTo(resourceIncrease));
    }
}
