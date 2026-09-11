using CardGameCore.Constants;
using CardGameCore.Framework;
using CardGameCore.Framework.Effects;

namespace CardGameCore.Impl.Cards;

public class CardImpl(
    CardName name,
    string description,
    IEffect effect,
    Dictionary<ResourceType, int> cost,
    Rarity rarity)
    : ICard
{
    public CardName Name { get; } = name;

    public IEffect Effect => effect.Copy();

    public string Description { get; } = description;
    public Dictionary<ResourceType, int> Cost { get; } = cost;
    public Rarity Rarity { get; } = rarity;
}