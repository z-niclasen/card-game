using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Impl;
using CardGame.Impl.Resources;
using CardGame.Library;

namespace CardGame.Test.Library;

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