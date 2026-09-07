using CardGameCore.Constants;
using CardGameCore.Impl;

namespace CardGameCore.Framework.Characters;

public interface ICharacterClass
{
    public CharacterName Name { get; }
    
    public Dictionary<ResourceType, IResource> InitialResources { get; }
    
    public IEnumerable<Tag> InitialTags { get; }
    
    public int InitialHandDrawCount { get; }
    
    public Deck StarterDeck { get; }
    IEnumerable<IRelic> InitialRelics { get; }
}