using CardGameCore.Constants;
using CardGameCore.Framework.Effects;
using CardGameCore.Impl.CombatEncounter;
using CardGameCore.Impl.Effects;

namespace CardGameCore.Library.Relics;

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