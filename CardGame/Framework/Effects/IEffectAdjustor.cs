using CardGame.Framework.Characters;

namespace CardGame.Framework.Effects;

public interface IEffectAdjustor
{
    public IEffect Adjust(IEffect effect, ICombatEncounter encounter, ICharacter target, ICharacter source);
}