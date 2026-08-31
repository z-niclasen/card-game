using System.ComponentModel;
using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Framework.Effects;

namespace CardGame.Impl.Effects;

public class EffectBuilder
{
    private readonly List<PrimitiveOrAdvanced> _effects = [];
    private string _description = "";
    private readonly List<Func<ICombatEncounter, ICharacter, ICharacter, List<IEffectPrimitive>>> _generators = [];

    public EffectBuilder DecreaseResource(ResourceType resourceType, int amount)
    {
        _effects.Add(new PrimitiveOrAdvanced(new DecreaseResourcePrimitive(resourceType, amount)));
        
        return this;
    }

    public EffectBuilder IncreaseResource(ResourceType resourceType, int amount)
    {
        _effects.Add(new PrimitiveOrAdvanced(new IncreaseResourcePrimitive(resourceType, amount)));
        return this;
    }

    public EffectBuilder NoneEffect()
    {
        _effects.Add(new PrimitiveOrAdvanced(new NonePrimitive()));
        return this;
    }

    public EffectBuilder PrimitiveEffect(IEffectPrimitive effect)
    {
        _effects.Add(new PrimitiveOrAdvanced(effect));
        return this;
    }

    public EffectBuilder CustomEffect(Func<ICombatEncounter, ICharacter, ICharacter, List<IEffectPrimitive>> generator)
    {
        _effects.Add(new PrimitiveOrAdvanced(generator));
        return this;
    }

    public EffectBuilder Description(string description)
    {
        _description = description;
        return this;
    }
    
    public EffectImpl Build()
    {
        if  (_effects.Count == 0)
            throw new InvalidOperationException("Cannot build with empty builder. Add primitives, custom, or both");
        if (_description == "")
            throw new InvalidOperationException("Cannot build with no description");

        return new EffectImpl(_description, PrimitiveGenerator);

        List<IEffectPrimitive> PrimitiveGenerator(ICombatEncounter encounter, ICharacter target, ICharacter source)
        {
            List<IEffectPrimitive> result = [];

            foreach (PrimitiveOrAdvanced effect in _effects)
            {
                switch (effect.Case)
                {
                    case PrimitiveOrAdvanced.CaseEnum.Primitive: result.Add(effect.Primitive!); break;
                    case PrimitiveOrAdvanced.CaseEnum.Advanced: result.AddRange(effect.Generator!(encounter, target, source)); break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            return result;
        }
    }
    
    private class PrimitiveOrAdvanced
    {
        public IEffectPrimitive? Primitive { get; }
        public Func<ICombatEncounter, ICharacter, ICharacter, List<IEffectPrimitive>>? Generator { get; }
        public CaseEnum Case { get; }

        public enum CaseEnum
        {
            Primitive, Advanced
        }

        public PrimitiveOrAdvanced(IEffectPrimitive primitive)
        {
            Primitive = primitive;
            Case = CaseEnum.Primitive;
        }

        public PrimitiveOrAdvanced(Func<ICombatEncounter, ICharacter, ICharacter, List<IEffectPrimitive>> generator)
        {
            Generator = generator;
            Case = CaseEnum.Advanced;
        }
        
        
    }
}