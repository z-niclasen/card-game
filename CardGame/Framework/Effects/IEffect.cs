using CardGame.Framework.Characters;

namespace CardGame.Framework.Effects;

public interface IEffect
{
    public string Description { get; }
    
    public List<IEffectPrimitive> GetPrimitives(ICombatEncounter encounter, ICharacter target, ICharacter source);
}