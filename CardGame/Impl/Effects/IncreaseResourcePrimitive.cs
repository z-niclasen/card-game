using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Framework.Effects;

namespace CardGame.Impl.Effects;

public class IncreaseResourcePrimitive(ResourceType resourceType, int value) : IEffectPrimitive
{
    public EffectType Type => EffectType.IncreaseResource;
    
    public ResourceType ResourceType { get; } = resourceType;

    public int Value { get; set; } = value;

    public void Apply(ICombatEncounter encounter, ICharacter target, ICharacter source)
    {
        encounter.IncreaseResourceForCharacter(target, ResourceType, Value);
    }
}