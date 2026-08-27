using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Framework.Effects;

namespace CardGame.Impl.Effects;

public class DecreaseResourcePrimitive : IEffectPrimitive
{
    public EffectType Type => EffectType.DecreaseResource;
    
    public ResourceType ResourceType { get; }
    
    public int Value { get; set; }
    
    private ICharacter Target { get; }

    public DecreaseResourcePrimitive(ICharacter target, ResourceType resourceType, int value)
    {
        Target = target;
        ResourceType = resourceType;
        Value = value;
    }
    
    public void Apply(ICombatEncounter encounter)
    {
        encounter.DecreaseResourceForCharacter(Target, ResourceType, Value);
    }
}