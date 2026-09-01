using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Framework.Effects;

namespace CardGame.Impl.Effects;

public class EffectImpl(
    List<IEffectPrimitive> primitives)
    : IEffect
{
    public List<IEffectPrimitive> Primitives { get; } = primitives;

    public void Apply(CombatTargetingContext ctx)
    {
        foreach (var primitive in Primitives)
        {
            primitive.Apply(ctx);
        }
    }

    public IEffect Copy()
    {
        List<IEffectPrimitive> primitivesCopy = Primitives.Select(primitive => primitive.Copy()).ToList();

        return new EffectImpl(primitivesCopy);
    }
}