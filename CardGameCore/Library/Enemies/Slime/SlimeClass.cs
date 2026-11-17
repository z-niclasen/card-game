using CardGameCore.Constants;
using CardGameCore.Framework.Characters;
using CardGameCore.Framework.Relics;
using CardGameCore.Framework.Resources;
using CardGameCore.Impl.Resources;

namespace CardGameCore.Library.Enemies.Slime;

public class SlimeClass : IAiCharacterClass
{
        public CharacterName Name => CharacterName.GreenSlime;

        public Dictionary<ResourceType, IResourceMutable> InitialResources => ResourceUtils.StandardResources(10, 1);

        public IEnumerable<Tag> InitialTags => [Tag.Slime];

        public int InitialHandDrawCount => 1;

        public IEnumerable<CardName> StarterDeck => [CardName.SlimeSpit, CardName.SlimeSpit];
        
        public IEnumerable<IRelic> InitialRelics => Enumerable.Empty<IRelic>();
        
        public AiStrategy Strategy { get; } = AiStrategy.PlayZero;
    
}