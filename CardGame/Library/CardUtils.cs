using CardGame.Constants;
using CardGame.Framework;
using CardGame.Impl;
using CardGame.Impl.Cards;
using CardGame.Impl.Effects;

namespace CardGame.Library;

public static class CardUtils
{
    public static ICard NewCard(string name, EffectImpl effect, int energyCost, Rarity rarity = Rarity.Common)
    {
        return new CardBuilder()
            .Name(name)
            .Effect(effect)
            .Cost(ResourceType.Energy, energyCost)
            .Build();
    }
    
    public static EffectImpl DamageEffect(int amount)
    {
        return new EffectBuilder()
                .DecreaseResource(ResourceType.Health, amount)
                .Build();
    }

    public static EffectImpl NoEffect()
    {
        return new EffectImpl([new NonePrimitive()]);
    }
}