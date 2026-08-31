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
    
    [SetUp]
    public void Setup()
    {
        ICharacterClass steveClass = new SteveClass();
        _steve = new CharacterImpl(steveClass);
        
        ICharacterClass greenSlimeClass = new GreenSlimeClass();
        _slime = new CharacterImpl(greenSlimeClass);

        _encounter = new CombatEncounterImpl(_steve, _slime);
    }
    
    [Test]
    public void AppliesDamageEffectCorrectly()
    {
        int slimeStartingHealth = _slime.GetHealth();
        const int damageAmount = 3;
        
        IEffectPrimitive damagePrimitive = new DecreaseResourcePrimitive(ResourceType.Health, damageAmount);
        damagePrimitive.Apply(_encounter, _slime, _steve);

        Assert.That(_slime.GetHealth(), Is.EqualTo(slimeStartingHealth - damageAmount));
    }
}