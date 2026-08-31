using CardGame.Constants;
using CardGame.Framework.Characters;
using CardGame.Impl;

namespace CardGame.Framework.Effects;

public interface IEffectPrimitive
{
    public EffectType Type { get; }
    
    public void Apply(CombatTargetingContext ctx);
}