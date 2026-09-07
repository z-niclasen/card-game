using CardGameCore.Constants;
using CardGameCore.Framework.Effects;
using CardGameCore.Impl.CombatEncounter;

namespace CardGameCore.Impl.Effects;

public class NonePrimitive : IEffectPrimitive
{
    public EffectType Type => EffectType.None;
    
    public void Apply(CombatTargetingContext ctx) { }
    public IEffectPrimitive Copy()
    {
        return new NonePrimitive();
    }
}