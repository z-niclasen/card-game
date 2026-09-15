using CardGameCore.Constants;
using CardGameCore.Framework;
using CardGameCore.Framework.Cards;
using CardGameCore.Impl.Cards;
using CardGameCore.Impl.Effects;

namespace CardGameCore.Library;

public static class CardUtils
{
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