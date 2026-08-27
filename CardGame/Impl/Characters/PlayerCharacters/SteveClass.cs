using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Impl.Resources;
using CardGame.Library;

namespace CardGame.Impl.Characters.PlayerCharacters;

public class SteveClass : ICharacterClass
{
    public CharacterName Name => CharacterName.Steve;

    public Dictionary<ResourceType, IResource> InitialResources => ResourceUtils.StandardResources(50, 3);

    public int InitialHandDrawCount => 5;

    public Deck StarterDeck => SteveCards.StarterDeck;
}