using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Framework.Effects;

namespace CardGame.Impl.Effects;

public class EffectImpl : IEffect
{
    public string Description { get; }
    
    private Func<ICombatEncounter, ICharacter, ICharacter, List<IEffectPrimitive>> PrimitiveGenerator { get; }

    public EffectImpl(string description, Func<ICombatEncounter, ICharacter, ICharacter, List<IEffectPrimitive>> primitiveGenerator)
    {
        Description = description;
        PrimitiveGenerator = primitiveGenerator;
    }
    
    
    public List<IEffectPrimitive> GetPrimitives(ICombatEncounter encounter, ICharacter target, ICharacter source)
    {
        return PrimitiveGenerator(encounter, target, source);
    }
}