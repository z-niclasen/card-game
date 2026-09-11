using CardGameCore.Constants;
using CardGameCore.Framework;
using CardGameCore.Impl.Cards;
using CardGameCore.Impl.Effects;

namespace CardGameCore.Library;

public static class CardUtils
{
    public static ICard NewCard(CardName name, EffectImpl effect, int energyCost, Rarity rarity = Rarity.Common)
    {
        return new CardBuilder()
            .Name(name)
            .Description("Placeholder Desscription")
            .Effect(effect)
            .Cost(ResourceType.Energy, energyCost)
            .Rarity(rarity)
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