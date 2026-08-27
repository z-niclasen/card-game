using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Effects;

namespace CardGame.Impl.Effects;

public class NonePrimitive : IEffectPrimitive
{
    public EffectType Type => EffectType.None;

    public int Value
    {
        get => throw new InvalidOperationException("None effect has no value."); 
        set => throw new InvalidOperationException("None effect has no value.");
    }
    
    public void Apply(ICombatEncounter encounter) { }
}