using CardGameCore.Constants;
using CardGameCore.Framework.Relics;
using CardGameCore.Framework.Resources;
using CardGameCore.Impl;

namespace CardGameCore.Framework.Characters;

public interface ICharacterClass
{
    public CharacterName Name { get; }
    
    public Dictionary<ResourceType, IResourceMutable> InitialResources { get; }
    
    public IEnumerable<Tag> InitialTags { get; }
    
    public int InitialHandDrawCount { get; }
    
    public IEnumerable<CardName> StarterDeck { get; }
    IEnumerable<IRelic> InitialRelics { get; }
}