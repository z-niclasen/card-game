using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Framework.Effects;

namespace CardGame.Impl.Effects;

public class IncreaseResourcePrimitive : IEffectPrimitive
{
    public EffectType Type => EffectType.IncreaseResource;
    
    public ResourceType ResourceType { get; }
    
    public int Value { get; set; }
    
    private ICharacter Target { get; }

    public IncreaseResourcePrimitive(ICharacter target, ResourceType resourceType, int value)
    {
        Target = target;
        ResourceType = resourceType;
        Value = value;
    }
    
    public void Apply(ICombatEncounter encounter)
    {
        encounter.IncreaseResourceForCharacter(Target, ResourceType, Value);
    }
}