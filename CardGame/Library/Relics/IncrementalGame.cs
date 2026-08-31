using CardGame.Constants;
using CardGame.Framework.Effects;
using CardGame.Impl;
using CardGame.Impl.Effects;

namespace CardGame.Library.Relics;

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