using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Framework.Effects;
using CardGame.Impl.Resources;

namespace CardGame.Impl.Effects;

public class ConditionalPrimitive(
    ConditionalPrimitive.ICondition condition,
    IEffectPrimitive ifEffect,
    IEffectPrimitive? elseEffect = null)
    : IEffectPrimitive
{
    public EffectType Type => EffectType.Conditional;
    
    public IEffectPrimitive IfEffect { get; } = ifEffect;
    public IEffectPrimitive? ElseEffect { get; } = elseEffect;

    public ICondition Condition { get; } = condition;

    public void Apply(CombatTargetingContext ctx)
    {
        if (Condition.Evaluate(ctx))
            IfEffect.Apply(ctx);
        else
            ElseEffect?.Apply(ctx);
    }

    public IEffectPrimitive Copy()
    {
        return new ConditionalPrimitive(Condition.Copy(), IfEffect.Copy(), ElseEffect?.Copy());
    }

    public interface ICondition
    {
        public bool Evaluate(CombatTargetingContext ctx);
        
        public ICondition Copy();
    }
    
    public class ConstantCondition(bool constant) : ICondition
    {
        public bool Evaluate(CombatTargetingContext ctx)
        {
            return constant;
        }

        public ICondition Copy()
        {
            return new ConstantCondition(constant);
        }
    }

    public enum Comparator
    {
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual
    }
    
    public class NumericThresholdCondition(ResourceType resourceType, int threshold, Comparator comparator, Target conditionTarget = Target.Target) : ICondition
    {
        public bool Evaluate(CombatTargetingContext ctx)
        {
            ICharacter target =  conditionTarget == Target.Target ? ctx.Target : ctx.Source;
            
            int resourceValue = target.GetResourceAmount(resourceType);

            return comparator switch
            {
                Comparator.LessThan => resourceValue < threshold,
                Comparator.LessThanOrEqual => resourceValue <= threshold,
                Comparator.GreaterThan => resourceValue > threshold,
                Comparator.GreaterThanOrEqual => resourceValue >= threshold,
                _ => throw new ArgumentOutOfRangeException(nameof(comparator), comparator, null)
            };
        }

        public ICondition Copy()
        {
            return new NumericThresholdCondition(resourceType, threshold, comparator, conditionTarget);
        }
    }
    
}