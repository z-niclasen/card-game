using CardGameCore.Constants;
using CardGameCore.Framework.Cards;
using CardGameCore.Framework.Effects;
using CardGameCore.Impl.Effects;

namespace CardGameCore.Library.Enemies.Slime.Cards;

public class SlimeSpitCard : ICard
{
    public CardName Name => CardName.SlimeSpit;
    public CharacterName Faction => CharacterName.GreenSlime;
    public IEffect Effect => new EffectBuilder().DecreaseResource(ResourceType.Health, 6).Build();
    public string Description => "";
    public Dictionary<ResourceType, int> Cost => new() { { ResourceType.Energy, 1 } };
    public Rarity Rarity => Rarity.Common;
}