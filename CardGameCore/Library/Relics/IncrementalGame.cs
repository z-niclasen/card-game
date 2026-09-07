using CardGameCore.Framework.Effects;
using CardGameCore.Impl.CombatEncounter;
using CardGameCore.Impl.Effects;

namespace CardGameCore.Library.Relics;

public class IncrementalGame : IEffectAdjustor
{
    public IEffect Adjust(IEffect effect, CombatTargetingContext ctx)
    {
        foreach (var primitive in effect.Primitives)
        {
            if (primitive is IncreaseResourcePrimitive p)
                p.Value++;
        }
        
        return effect;
    }
}