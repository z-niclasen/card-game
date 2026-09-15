using CardGameCore.Constants;
using CardGameCore.Framework;
using CardGameCore.Framework.Characters;
using CardGameCore.Framework.Relics;
using CardGameCore.Framework.Resources;
using CardGameCore.Impl;
using CardGameCore.Impl.Resources;
using CardGameCore.Library.Relics;

namespace CardGameCore.Library.Characters.PlayerCharacters;

public class SteveClass : ICharacterClass
{
    public CharacterName Name => CharacterName.Steve;

    public Dictionary<ResourceType, IResourceMutable> InitialResources => ResourceUtils.StandardResources(50, 3);
    
    public IEnumerable<Tag> InitialTags => [Tag.PlayerCharacter];

    public int InitialHandDrawCount => 5;

    public IEnumerable<CardName> StarterDeck => [
        CardName.Sword, CardName.Sword, CardName.Sword, CardName.Sword, CardName.Sword, CardName.Sword, CardName.Sword, 
        CardName.Sword, CardName.Sword, CardName.Sword, CardName.Sword, CardName.Sword];
    
    public IEnumerable<IRelic> InitialRelics => [new ChaliceRelic(), new ShieldRelic()];
}