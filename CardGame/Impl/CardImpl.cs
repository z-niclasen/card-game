using CardGame.Constants;
using CardGame.Framework;

namespace CardGame.Impl;

public class CardImpl : ICard
{
    public string Name { get; }
    
    public IEffect Effect { get; }

    public string Description => Effect.Description;
    public int Cost { get; }
    public Rarity Rarity { get; }

    public CardImpl(string name, IEffect effect, int cost, Rarity rarity)
    {
        Name = name;
        Effect = effect;
        Cost = cost;
        Rarity = rarity;
    }
}