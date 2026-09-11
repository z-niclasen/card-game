using CardGameCore.Framework.Effects;
using CardGameCore.Framework.Relics;

namespace CardGameCore.Impl.Relics;

public class RelicCollection : IRelicCollectionMutable
{
    public event RelicAddedDelegate? OnRelicAdded;
    public event RelicRemovedDelegate? OnRelicRemoved;
    
    public IEnumerable<IRelic> Relics => _relics;
    private readonly List<IRelic> _relics = [];

    public IEnumerable<IEffectAdjustor> Offensive => _offensiveEffectAdjustors;
    private readonly List<IEffectAdjustor> _offensiveEffectAdjustors = [];
    
    public IEnumerable<IEffectAdjustor> Defensive => _defensiveEffectAdjustors;
    private readonly List<IEffectAdjustor> _defensiveEffectAdjustors = [];

    public void AddRelic(IRelic relic)
    {
        _relics.Add(relic);
        
        _offensiveEffectAdjustors.AddRange(relic.Offensive);
        _defensiveEffectAdjustors.AddRange(relic.Defensive);
        
        InvokeRelicAdded(relic);
    }
    
    public void AddRelics(IEnumerable<IRelic> startingRelics)
    {
        foreach (IRelic relicToAdd in startingRelics)
            AddRelic(relicToAdd);
    }

    public void RemoveRelic(IRelic relic)
    {
        if (!_relics.Contains(relic))
            throw new ArgumentException($"Tried to remove relic {relic.Name}, but it does not lie in collection.");
        
        _relics.Remove(relic);
        
        foreach (IEffectAdjustor adjustorToRemove in relic.Offensive)
            _offensiveEffectAdjustors.Remove(adjustorToRemove);
        
        foreach (IEffectAdjustor adjustorToRemove in relic.Defensive)
            _defensiveEffectAdjustors.Remove(adjustorToRemove);
        
        InvokeRelicRemoved(relic);
    }

    private void InvokeRelicAdded(IRelic relic)
    {
        OnRelicAdded?.Invoke(relic);
    }

    private void InvokeRelicRemoved(IRelic relic)
    {
        OnRelicRemoved?.Invoke(relic);
    }
}