using CardGameCore.Framework;

namespace CardGameCore.Test.CombatEncounter;

public class CombatEncounterSpy
{
    private ICombatEncounter _encounter;
    
    public List<string> _messages = [];

    public CombatEncounterSpy(ICombatEncounter encounter)
    {
        _encounter = encounter;

        _encounter.OnEndTurn += OnEndEncounterTurn;
    }

    private void OnEndEncounterTurn(ICombatEncounter encounter)
    {
        _messages.Add("OnEndTurn");
    }
}