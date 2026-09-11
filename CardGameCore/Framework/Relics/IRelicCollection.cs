using CardGameCore.Framework.Effects;

namespace CardGameCore.Framework.Relics;

public delegate void RelicAddedDelegate(IRelic relic);
public delegate void RelicRemovedDelegate(IRelic relic);

public interface IRelicCollection
{
    public event RelicAddedDelegate? OnRelicAdded;
    public event RelicRemovedDelegate? OnRelicRemoved;
    
    public IEnumerable<IRelic> Relics { get; }

    public IEnumerable<IEffectAdjustor> Offensive { get; }
    
    public IEnumerable<IEffectAdjustor> Defensive { get; }
}