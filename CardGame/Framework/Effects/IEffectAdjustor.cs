using CardGame.Framework.Characters;
using CardGame.Impl;

namespace CardGame.Framework.Effects;

public interface IEffectAdjustor
{
    public IEffect Adjust(IEffect effect, CombatTargetingContext ctx);
}