using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Effects;

namespace CardGame.Impl.Cards;

public class CardImpl(
    string name,
    string description,
    IEffect effect,
    Dictionary<ResourceType, int> cost,
    Rarity rarity)
    : ICard
{
    public string Name { get; } = name;

    public IEffect Effect => effect.Copy();

    public string Description { get; } = description;
    public Dictionary<ResourceType, int> Cost { get; } = cost;
    public Rarity Rarity { get; } = rarity;
}