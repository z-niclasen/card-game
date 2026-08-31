using System.ComponentModel;
using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Framework.Effects;

namespace CardGame.Impl.Effects;

public class EffectBuilder
{
    private readonly List<IEffectPrimitive> _effects = [];
    
    
    public EffectBuilder DecreaseResource(ResourceType resourceType, int amount)
    {
        _effects.Add(new DecreaseResourcePrimitive(resourceType, amount));
        
        return this;
    }

    public EffectBuilder IncreaseResource(ResourceType resourceType, int amount)
    {
        _effects.Add(new IncreaseResourcePrimitive(resourceType, amount));
        return this;
    }

    public EffectBuilder NoneEffect()
    {
        _effects.Add(new NonePrimitive());
        return this;
    }

    public EffectBuilder PrimitiveEffect(IEffectPrimitive effect)
    {
        _effects.Add(effect);
        return this;
    }

    public EffectBuilder PrimitiveEffects(List<IEffectPrimitive> effects)
    {
        _effects.AddRange(effects);
        return this;
    }

    public EffectImpl Build()
    {
        if (_effects.Count == 0)
            throw new InvalidOperationException("Cannot build with empty builder. Add primitives");

        return new EffectImpl(_effects);
    }
}