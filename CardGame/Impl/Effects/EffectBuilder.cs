using System.ComponentModel;
using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Framework.Effects;

namespace CardGame.Impl.Effects;

public class EffectBuilder
{

    private readonly List<IEffectPrimitive> _effects = [];
    private string _description = "";
    private readonly List<Func<ICombatEncounter, ICharacter, ICharacter, List<IEffectPrimitive>>> _generators = [];
    
    public EffectBuilder()
    {
        
    }

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

    public EffectBuilder CustomEffect(Func<ICombatEncounter, ICharacter, ICharacter, List<IEffectPrimitive>> generator)
    {
        _generators.Add(generator);
        return this;
    }

    public EffectBuilder Description(string description)
    {
        _description = description;
        return this;
    }
    
    public EffectImpl Build()
    {
        if  (_effects.Count == 0 && _generators.Count == 0)
            throw new InvalidOperationException("Cannot build with empty builder. Add primitives, custom, or both");
        if (_description == "")
            throw new InvalidOperationException("Cannot build with no description");

        return new EffectImpl(_description, PrimitiveGenerator);

        List<IEffectPrimitive> PrimitiveGenerator(ICombatEncounter encounter, ICharacter target, ICharacter source)
        {
            List<IEffectPrimitive> result = [];
            if (_generators.Count != 0)
            {
                foreach (var generator in _generators)
                {
                    result.AddRange(generator(encounter, target, source));
                }
            }

            result.AddRange(_effects);
            return result;
        }
    }
}