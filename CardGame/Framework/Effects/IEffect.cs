using CardGame.Framework.Characters;
using CardGame.Impl;

namespace CardGame.Framework.Effects;

public interface IEffect
{
    public List<IEffectPrimitive> Primitives { get; }
    
    public void Apply(CombatTargetingContext ctx);

    public IEffect Copy();
}