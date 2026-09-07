using CardGameCore.Constants;
using CardGameCore.Impl.CombatEncounter;

namespace CardGameCore.Framework.Effects;

public interface IEffectPrimitive
{
    public EffectType Type { get; }
    
    public void Apply(CombatTargetingContext ctx);

    public IEffectPrimitive Copy();
}