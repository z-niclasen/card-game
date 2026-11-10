using CardGameCore.Constants;
using CardGameCore.Framework.Effects;
using CardGameCore.Framework.Relics;

namespace CardGameCore.Library.Relics;

public class ChaliceRelic : IRelic
{
    public RelicName Name => RelicName.Chalice;
    public string Description => "Gør ting.";
    public Rarity Rarity => Rarity.SuperDuperRareWowAwooga;

    public CharacterName Faction => CharacterName.Neutral;
    
    public IEnumerable<IEffectAdjustor> Offensive { get; } = [new IncrementalGame()];
    public IEnumerable<IEffectAdjustor> Defensive { get; } = [];
}