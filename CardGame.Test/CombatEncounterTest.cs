using CardGame.Constants;
using CardGame.Exceptions;
using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Impl;
using CardGame.Library.Characters.EnemyCharacters;
using CardGame.Library.Characters.PlayerCharacters;

namespace CardGame.Test;

public class CombatEncounterTest
{
    private ICharacter _steve;
    private ICharacter _slime;
    private ICombatEncounter _encounter;

    private CombatTargetingContext _steveTargetsSlime;
    private CombatTargetingContext _slimeTargetsSteve;
    
    [SetUp]
    public void Setup()
    {
        ICharacterClass steveClass = new SteveClass();
        _steve = new CharacterImpl(steveClass);
        
        ICharacterClass greenSlimeClass = new GreenSlimeClass();
        _slime = new CharacterImpl(greenSlimeClass);

        _encounter = new CombatEncounterImpl(_steve, _slime);

        _steveTargetsSlime = new CombatTargetingContext(_encounter, _slime, _steve);
        _slimeTargetsSteve = new CombatTargetingContext(_encounter, _steve, _slime);
    }

    [Test]
    public void CanPlayBasicEncounter()
    {
        int steveStartHealth = _steve.Health;
        int steveStartEnergy = _steve.Energy;
        int slimeStartHealth = _slime.Health;
        int slimeStartEnergy = _slime.Energy;
        
        _encounter.PlayCardFromHandAtIndex(_steve, 0, _slime);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(_slime.Health, Is.EqualTo(slimeStartHealth - 2));
            Assert.That(_steve.Energy, Is.EqualTo(steveStartEnergy - 1));
        }
        
        _encounter.PlayCardFromHandAtIndex(_steve, 0, _slime);
        _encounter.PlayCardFromHandAtIndex(_steve, 0, _slime);
        
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_slime.Health, Is.EqualTo(slimeStartHealth - 2 * 3));
            Assert.That(_steve.Energy, Is.EqualTo(steveStartEnergy - 1 * 3));
        }
        
        Assert.Throws<NotEnoughResourcesException>(() => _encounter.PlayCardFromHandAtIndex(_steve, 0, _slime));
        
        Assert.That(_encounter.IsFinished, Is.Not.True);
        _encounter.EndTurn(_steve);
        Assert.That(_encounter.IsFinished, Is.Not.True);

        _encounter.PlayCardFromHandAtIndex(_slime, 0, _steve);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(_slime.Energy, Is.EqualTo(slimeStartEnergy - 1));
            Assert.That(_steve.Health, Is.EqualTo(steveStartHealth - 6));
        }
        
        Assert.That(_encounter.IsFinished, Is.Not.True);
        _encounter.EndTurn(_slime);
        Assert.That(_encounter.IsFinished, Is.Not.True);
        
        Assert.That(_steve.Energy, Is.EqualTo(steveStartEnergy));
        
        _encounter.PlayCardFromHandAtIndex(_steve, 0, _slime);
        _encounter.PlayCardFromHandAtIndex(_steve, 0, _slime);
        
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_slime.Health, Is.EqualTo(slimeStartHealth - 2 * 5));
            Assert.That(_steve.Energy, Is.EqualTo(steveStartEnergy - 1 * 2));
            Assert.That(_encounter.IsFinished, Is.Not.True);
        }
        
        _encounter.PlayCardFromHandAtIndex(_steve, 0, _slime);
        
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_slime.Health, Is.EqualTo(slimeStartHealth - 2 * 6));
            Assert.That(_steve.Energy, Is.EqualTo(steveStartEnergy - 1 * 3));
            Assert.That(_encounter.IsFinished, Is.Not.True);
        }
        
        _encounter.EndTurn(_steve);
        Assert.That(_encounter.IsFinished, Is.True);

        Assert.Throws<CombatEncounterInactiveException>(() => _encounter.PlayCardFromHandAtIndex(_slime, 0, _steve));
    }

    [Test]
    public void CharactersAndEncounterHaveInitialValues()
    {
        Assert.That(_steve.GetResourceAmount(ResourceType.Health), Is.EqualTo(50));
    }

    [Test]
    public void CombatCardsDrawingLogic()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_encounter.GetHandCountOfCharacter(_steve), Is.EqualTo(_steve.HandDrawCount));
            Assert.That(_encounter.GetHandCountOfCharacter(_slime), Is.EqualTo(0));

            Assert.That(_encounter.GetDiscardPileCountOfCharacter(_steve), Is.EqualTo(0));
            Assert.That(_encounter.GetDiscardPileCountOfCharacter(_slime), Is.EqualTo(0));
            Assert.That(_encounter.GetExhaustPileCountOfCharacter(_steve), Is.EqualTo(0));
            Assert.That(_encounter.GetExhaustPileCountOfCharacter(_slime), Is.EqualTo(0));

            Assert.That(_encounter.GetDrawPileCountOfCharacter(_steve), Is.EqualTo(_steve.Deck.Count - _steve.HandDrawCount));
            Assert.That(_encounter.GetDrawPileCountOfCharacter(_slime), Is.EqualTo(_slime.Deck.Count));
        }
        
        _encounter.EndTurn(_steve);
        
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_encounter.GetHandCountOfCharacter(_steve), Is.EqualTo(_steve.HandDrawCount));
            Assert.That(_encounter.GetHandCountOfCharacter(_slime), Is.EqualTo(_slime.HandDrawCount));

            Assert.That(_encounter.GetDiscardPileCountOfCharacter(_steve), Is.EqualTo(0));
            Assert.That(_encounter.GetDiscardPileCountOfCharacter(_slime), Is.EqualTo(0));
            Assert.That(_encounter.GetExhaustPileCountOfCharacter(_steve), Is.EqualTo(0));
            Assert.That(_encounter.GetExhaustPileCountOfCharacter(_slime), Is.EqualTo(0));

            Assert.That(_encounter.GetDrawPileCountOfCharacter(_steve), Is.EqualTo(_steve.Deck.Count - _steve.HandDrawCount));
            Assert.That(_encounter.GetDrawPileCountOfCharacter(_slime), Is.EqualTo(_slime.Deck.Count - _slime.HandDrawCount));
        }
        
        _encounter.EndTurn(_slime);
        
        using (Assert.EnterMultipleScope())
        {
            Assert.That(_encounter.GetHandCountOfCharacter(_steve), Is.EqualTo(_steve.HandDrawCount));
            Assert.That(_encounter.GetHandCountOfCharacter(_slime), Is.EqualTo(_slime.HandDrawCount));

            Assert.That(_encounter.GetDiscardPileCountOfCharacter(_steve), Is.EqualTo(_steve.HandDrawCount));
            Assert.That(_encounter.GetDiscardPileCountOfCharacter(_slime), Is.EqualTo(0));
            Assert.That(_encounter.GetExhaustPileCountOfCharacter(_steve), Is.EqualTo(0));
            Assert.That(_encounter.GetExhaustPileCountOfCharacter(_slime), Is.EqualTo(0));

            Assert.That(_encounter.GetDrawPileCountOfCharacter(_steve), Is.EqualTo(_steve.Deck.Count - 2 * _steve.HandDrawCount));
            Assert.That(_encounter.GetDrawPileCountOfCharacter(_slime), Is.EqualTo(_slime.Deck.Count - _slime.HandDrawCount));
        }
    }
}