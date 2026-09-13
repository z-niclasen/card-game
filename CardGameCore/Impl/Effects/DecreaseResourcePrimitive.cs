using CardGameCore.Constants;
using CardGameCore.Framework.Effects;
using CardGameCore.Impl.CombatEncounter;

namespace CardGameCore.Impl.Effects;

public class DecreaseResourcePrimitive(ResourceType resourceType, int value) : IEffectPrimitive
{
    public EffectType Type => EffectType.DecreaseResource;
    
    public ResourceType ResourceType { get; } = resourceType;

    public int Value { get; set; } = value;
    
    public void Apply(CombatTargetingContext ctx)
    {
        bool damagingWithArmor = ResourceType == ResourceType.Health && ctx.Target.HasResourceType(ResourceType.Armor);
        if (damagingWithArmor)
            Value = Math.Max(0, Value - ctx.Target.GetResourceAmount(ResourceType.Armor));
        
        ctx.Encounter.DecreaseResourceForCharacter(ctx.Target, ResourceType, Value);
    }

    public IEffectPrimitive Copy()
    {
        return new DecreaseResourcePrimitive(ResourceType, Value);
    }
}