using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Framework.Effects;

namespace CardGame.Impl.Effects;

public class NonePrimitive : IEffectPrimitive
{
    public EffectType Type => EffectType.None;
    
    public void Apply(CombatTargetingContext ctx) { }
}