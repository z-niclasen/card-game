using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Impl;
using CardGame.Impl.Characters.EnemyCharacters;
using CardGame.Impl.Characters.PlayerCharacters;

namespace CardGame.Test;

public class CombatEncounterTest
{
    private ICharacter steve;
    private ICharacter slime;
    private ICombatEncounter encounter;
    
    [SetUp]
    public void Setup()
    {
        ICharacterClass steveClass = new SteveClass();
        steve = new CharacterImpl(steveClass);
        
        ICharacterClass greenSlimeClass = new GreenSlimeClass();
        slime = new CharacterImpl(greenSlimeClass);

        encounter = new CombatEncounterImpl(steve, slime);
    }

    [Test]
    public void CharactersAndEncounterHaveInitialValues()
    {
        Assert.That(steve.GetResourceAmount(ResourceType.Health), Is.EqualTo(50));
    }

    [Test]
    public void CombatCardsDrawingLogic()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(encounter.GetHandCountOfCharacter(steve), Is.EqualTo(steve.HandDrawCount));
            Assert.That(encounter.GetHandCountOfCharacter(slime), Is.EqualTo(0));

            Assert.That(encounter.GetDiscardPileCountOfCharacter(steve), Is.EqualTo(0));
            Assert.That(encounter.GetDiscardPileCountOfCharacter(slime), Is.EqualTo(0));
            Assert.That(encounter.GetExhaustPileCountOfCharacter(steve), Is.EqualTo(0));
            Assert.That(encounter.GetExhaustPileCountOfCharacter(slime), Is.EqualTo(0));

            Assert.That(encounter.GetDrawPileCountOfCharacter(steve), Is.EqualTo(steve.Deck.Count - steve.HandDrawCount));
            Assert.That(encounter.GetDrawPileCountOfCharacter(slime), Is.EqualTo(slime.Deck.Count));
        }
        
        encounter.EndTurn(steve);
        
        using (Assert.EnterMultipleScope())
        {
            Assert.That(encounter.GetHandCountOfCharacter(steve), Is.EqualTo(steve.HandDrawCount));
            Assert.That(encounter.GetHandCountOfCharacter(slime), Is.EqualTo(slime.HandDrawCount));

            Assert.That(encounter.GetDiscardPileCountOfCharacter(steve), Is.EqualTo(0));
            Assert.That(encounter.GetDiscardPileCountOfCharacter(slime), Is.EqualTo(0));
            Assert.That(encounter.GetExhaustPileCountOfCharacter(steve), Is.EqualTo(0));
            Assert.That(encounter.GetExhaustPileCountOfCharacter(slime), Is.EqualTo(0));

            Assert.That(encounter.GetDrawPileCountOfCharacter(steve), Is.EqualTo(steve.Deck.Count - steve.HandDrawCount));
            Assert.That(encounter.GetDrawPileCountOfCharacter(slime), Is.EqualTo(slime.Deck.Count - slime.HandDrawCount));
        }
        
        encounter.EndTurn(slime);
        
        using (Assert.EnterMultipleScope())
        {
            Assert.That(encounter.GetHandCountOfCharacter(steve), Is.EqualTo(steve.HandDrawCount));
            Assert.That(encounter.GetHandCountOfCharacter(slime), Is.EqualTo(slime.HandDrawCount));

            Assert.That(encounter.GetDiscardPileCountOfCharacter(steve), Is.EqualTo(steve.HandDrawCount));
            Assert.That(encounter.GetDiscardPileCountOfCharacter(slime), Is.EqualTo(0));
            Assert.That(encounter.GetExhaustPileCountOfCharacter(steve), Is.EqualTo(0));
            Assert.That(encounter.GetExhaustPileCountOfCharacter(slime), Is.EqualTo(0));

            Assert.That(encounter.GetDrawPileCountOfCharacter(steve), Is.EqualTo(steve.Deck.Count - 2 * steve.HandDrawCount));
            Assert.That(encounter.GetDrawPileCountOfCharacter(slime), Is.EqualTo(slime.Deck.Count - slime.HandDrawCount));
        }
    }
}