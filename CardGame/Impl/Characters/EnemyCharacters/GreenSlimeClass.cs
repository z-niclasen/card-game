using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Impl.Resources;
using CardGame.Library;

namespace CardGame.Impl.Characters.EnemyCharacters;

public class GreenSlimeClass : ICharacterClass
{
    public CharacterName Name => CharacterName.GreenSlime;

    public Dictionary<ResourceType, IResource> InitialResources => ResourceUtils.StandardResources(10, 1);

    public int InitialHandDrawCount => 1;

    public Deck StarterDeck => GreenSlimeCards.StarterDeck;
}