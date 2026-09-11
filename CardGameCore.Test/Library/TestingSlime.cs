using CardGameCore.Constants;
using CardGameCore.Framework;
using CardGameCore.Framework.Characters;
using CardGameCore.Framework.Relics;
using CardGameCore.Impl;
using CardGameCore.Impl.Resources;
using CardGameCore.Library;

namespace CardGameCore.Test.Library;

public class TestingSlime(AiStrategy strategy) : IAiCharacterClass
{
    public CharacterName Name => CharacterName.GreenSlime;

    public Dictionary<ResourceType, IResource> InitialResources => ResourceUtils.StandardResources(10, 1);

    public IEnumerable<Tag> InitialTags => [Tag.Slime];

    public int InitialHandDrawCount => 1;

    public Deck StarterDeck => GreenSlimeCards.StarterDeck;
    
    public IEnumerable<IRelic> InitialRelics => Enumerable.Empty<IRelic>();
    public AiStrategy Strategy { get; } = strategy;
}