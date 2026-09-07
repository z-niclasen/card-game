using CardGameCore.Constants;
using CardGameCore.Framework;
using CardGameCore.Framework.Characters;
using CardGameCore.Impl;
using CardGameCore.Impl.Resources;

namespace CardGameCore.Library.Characters.PlayerCharacters;

public class SteveClass : ICharacterClass
{
    public CharacterName Name => CharacterName.Steve;

    public Dictionary<ResourceType, IResource> InitialResources => ResourceUtils.StandardResources(50, 3);
    
    public IEnumerable<Tag> InitialTags => [Tag.PlayerCharacter];

    public int InitialHandDrawCount => 5;

    public Deck StarterDeck => SteveCards.StarterDeck;
    
    public IEnumerable<IRelic> InitialRelics => Enumerable.Empty<IRelic>();
}