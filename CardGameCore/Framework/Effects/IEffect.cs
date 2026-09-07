using CardGameCore.Impl.CombatEncounter;

namespace CardGameCore.Framework.Effects;

public interface IEffect
{
    public List<IEffectPrimitive> Primitives { get; }
    
    public void Apply(CombatTargetingContext ctx);

    public IEffect Copy();
}