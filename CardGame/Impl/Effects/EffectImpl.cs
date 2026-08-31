using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Framework.Effects;

namespace CardGame.Impl.Effects;

public class EffectImpl(
    string description,
    Func<ICombatEncounter, ICharacter, ICharacter, List<IEffectPrimitive>> primitiveGenerator)
    : IEffect
{
    public string Description { get; } = description;

    private Func<ICombatEncounter, ICharacter, ICharacter, List<IEffectPrimitive>> PrimitiveGenerator { get; } = primitiveGenerator;


    public List<IEffectPrimitive> GetPrimitives(ICombatEncounter encounter, ICharacter target, ICharacter source)
    {
        return PrimitiveGenerator(encounter, target, source);
    }
}