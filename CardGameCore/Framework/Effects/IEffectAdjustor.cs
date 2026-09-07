using CardGameCore.Impl.CombatEncounter;

namespace CardGameCore.Framework.Effects;

public interface IEffectAdjustor
{
    public IEffect Adjust(IEffect effect, CombatTargetingContext ctx);
}