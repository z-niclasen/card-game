using CardGameCore.Framework;
using CardGameCore.Framework.CombatEncounter;
using CardGameCore.Impl.CombatEncounter;

namespace CardGameCore.Test.CombatEncounter;

public class CombatEncounterSpy
{
    private ICombatEncounter _encounter;
    
    public List<string> _messages = [];

    public CombatEncounterSpy(ICombatEncounter encounter)
    {
        _encounter = encounter;

        _encounter.OnEndTurn += EncounterOnEndTurn;
        _encounter.OnPlayCard += EncounterOnOnPlayCard;
        _encounter.OnEncounterFinished += EncounterOnOnEncounterFinished;
    }

    private void EncounterOnOnEncounterFinished(ICombatEncounter encounter)
    {
        _messages.Add("OnEncounterFinished");
    }

    private void EncounterOnOnPlayCard(ICombatEncounter encounter, ICard playedCard, CombatTargetingContext ctx)
    {
        _messages.Add("OnPlayCard");
    }

    private void EncounterOnEndTurn(ICombatEncounter encounter)
    {
        _messages.Add("OnEndTurn");
    }
}