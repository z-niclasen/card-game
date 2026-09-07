using CardGameCore.Constants;
using CardGameCore.Framework.Effects;
using CardGameCore.Impl.CombatEncounter;

namespace CardGameCore.Impl.Effects;

public class IncreaseResourcePrimitive(ResourceType resourceType, int value) : IEffectPrimitive
{
    public EffectType Type => EffectType.IncreaseResource;
    
    public ResourceType ResourceType { get; } = resourceType;

    public int Value { get; set; } = value;

    public void Apply(CombatTargetingContext ctx)
    {
        ctx.Encounter.IncreaseResourceForCharacter(ctx.Target, ResourceType, Value);
    }

    public IEffectPrimitive Copy()
    {
        return new IncreaseResourcePrimitive(ResourceType, Value);
    }
}