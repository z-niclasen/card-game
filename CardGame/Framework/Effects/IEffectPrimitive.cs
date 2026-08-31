using CardGame.Constants;
using CardGame.Framework.Characters;

namespace CardGame.Framework.Effects;

public interface IEffectPrimitive
{
    public EffectType Type { get; }
    
    public int Value { get; set; }
    
    public void Apply(ICombatEncounter encounter, ICharacter target, ICharacter source);
}