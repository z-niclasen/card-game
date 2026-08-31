using CardGame.Constants;
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