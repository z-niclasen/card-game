using CardGameCore.Framework.Cards;
using CardGameCore.Framework.Relics;

namespace CardGameCore.Framework.CombatEncounter;

public interface ICombatEncounterReward
{
    public IEnumerable<ICardReward> GetCardRewards();

    public IEnumerable<IRelicReward> GetRelicRewards();
}