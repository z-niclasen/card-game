using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Impl;
using CardGame.Impl.Resources;

namespace CardGame.Library.Characters.PlayerCharacters;

public class SteveClass : ICharacterClass
{
    public CharacterName Name => CharacterName.Steve;

    public Dictionary<ResourceType, IResource> InitialResources => ResourceUtils.StandardResources(50, 3);
    
    public IEnumerable<Tag> InitialTags => [Tag.PlayerCharacter];

    public int InitialHandDrawCount => 5;

    public Deck StarterDeck => SteveCards.StarterDeck;
    
    public IEnumerable<IRelic> InitialRelics => Enumerable.Empty<IRelic>();
}