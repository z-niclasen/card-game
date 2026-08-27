using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Effects;

namespace CardGame.Impl;

public class CardImpl : ICard
{
    public string Name { get; }
    
    public IEffect Effect { get; }

    public string Description => Effect.Description;
    public Dictionary<ResourceType, int> Cost { get; }
    public Rarity Rarity { get; }

    public CardImpl(string name, IEffect effect, Dictionary<ResourceType, int> cost, Rarity rarity)
    {
        Name = name;
        Effect = effect;
        Cost = cost;
        Rarity = rarity;
    }
}