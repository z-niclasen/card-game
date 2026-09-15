using CardGameCore.Constants;
using CardGameCore.Framework.Effects;
using CardGameCore.Framework.Relics;
using CardGameCore.Impl.Relics;

namespace CardGameCore.Library.Relics;

public class ShieldRelic : IRelic
{ 
    public RelicName Name => RelicName.Shield;
    
    public string Description => "Ser sej ud.";
    
    public Rarity Rarity => Rarity.Rare;

    public IEnumerable<IEffectAdjustor> Offensive => [new Kleenex()];

    public IEnumerable<IEffectAdjustor> Defensive => [];
}