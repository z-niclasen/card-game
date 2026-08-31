using CardGame.Constants;
using CardGame.Framework.Effects;
using CardGame.Impl;
using CardGame.Impl.Effects;

namespace CardGame.Library.Relics;

public class Kleenex : IEffectAdjustor
{
    public IEffect Adjust(IEffect effect, CombatTargetingContext ctx)
    {
        if (!ctx.Target.Tags.Contains(Tag.Slime))
            return effect;

        foreach (var primitive in effect.Primitives)
        {
            if (primitive is DecreaseResourcePrimitive { ResourceType: ResourceType.Health } p)
                p.Value *= 2;
        }

        return effect;
    }
}