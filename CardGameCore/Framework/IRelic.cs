using CardGameCore.Constants;
using CardGameCore.Framework.Effects;

namespace CardGameCore.Framework;

public interface IRelic
{
    public RelicName Name { get; }
    
    public string Description { get; }
    
    public Rarity Rarity { get; }
    
    public IEnumerable<IEffectAdjustor> Offensive { get; }
    
    public IEnumerable<IEffectAdjustor> Defensive { get; }
}