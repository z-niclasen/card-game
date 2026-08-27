using CardGame.Constants;
using CardGame.Framework;
using CardGame.Impl;
using CardGame.Impl.Effects;

namespace CardGame.Library;

public class CardUtils
{
    public static ICard NewCard(string name, EffectImpl effect, int energyCost, Rarity rarity = Rarity.Common)
    {
        return new CardImpl(
            name,
            effect,
            new Dictionary<ResourceType, int> { { ResourceType.Energy, energyCost } },
            rarity
        );
    }
    
    public static EffectImpl DamageEffect(int amount)
    {
        return new EffectImpl($"Deal {amount} damage.",
            (_, _, target) => [new DecreaseResourcePrimitive(target, ResourceType.Health, amount)]);
    }

    public static EffectImpl NoEffect()
    {
        return new EffectImpl("Does nothing.", 
            (_, _, target) => [new NonePrimitive()]);
    }
}