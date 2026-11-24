using System.Collections.Generic;
using CardGameCore;
using CardGameCore.Framework.CombatEncounter;
using CardGameCore.Framework.Runs;
using CardGameCore.Impl.CombatEncounter;
using CardGameCore.Impl.Runs;
using Godot;

namespace CardGame;

public partial class RunUI : Node
{
    public static RunUI Instance { get; private set; }
    
    private Node CurrentScene { get; set; }

    private PackedScene _mapUIScene;
    private PackedScene _combatEncounterUIScene;

    public override void _Ready()
    {
        Instance = this;

        _mapUIScene = GD.Load<PackedScene>("uid://bn2v8jntpuame");
        _combatEncounterUIScene = GD.Load<PackedScene>("uid://bq1gssncw76xv");
        
        GoToScene(_mapUIScene);
    }

    private void OnEncounterButtonPressed(Map.MapPosition position)
    {
        GD.Print(position);
        if (!Run.Instance.CanMoveTo(position))
            return;
        
        Run.Instance.MoveTo(position);

        if (Run.Instance.CurrentEncounter is ICombatEncounter combatEncounter)
        {
            GoToScene(_combatEncounterUIScene);
            CombatEncounterUI combatEncounterUI = (CombatEncounterUI)CurrentScene;
            combatEncounterUI.SetEncounter(combatEncounter);
        }
        
        Run.Instance.CurrentEncounter.OnRewardsSelected += OnRewardsSelected;
    }

    private void OnRewardsSelected(List<IReward> rewards)
    {
        GoToScene(_mapUIScene);
    }

    private void GoToScene(PackedScene scene)
    {
        CallDeferred(nameof(DeferredGoToScene), scene);
    }

    private void DeferredGoToScene(PackedScene scene)
    {
        CurrentScene?.Free();

        CurrentScene = scene.Instantiate();
        GetTree().Root.AddChild(CurrentScene);

        if (CurrentScene is MapUI mapUI)
        {
            mapUI.OnEncounterButtonPressed += OnEncounterButtonPressed;
        }
    }
}