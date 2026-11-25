using System.Collections.Generic;
using CardGameCore.Framework.CombatEncounter;
using CardGameCore.Framework.Runs;
using CardGameCore.Impl;
using CardGameCore.Impl.CombatEncounter;
using CardGameCore.Impl.Runs;
using Godot;
using Map = CardGameCore.Map;

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
        if (!Run.Instance.CanMoveTo(position))
            return;
        
        Run.Instance.MoveTo(position);

        if (Run.Instance.CurrentEncounter is ICombatEncounter combatEncounter)
        {
            GoToScene(_combatEncounterUIScene);
        }
    }

    private void GoToScene(PackedScene scene)
    {
        CallDeferred(nameof(DeferredGoToScene), scene);
    }

    private void DeferredGoToScene(PackedScene scene)
    {
        RemoveObserversFromCurrentScene();
        
        CurrentScene?.Free();

        CurrentScene = scene.Instantiate();
        GetTree().Root.AddChild(CurrentScene);

        AddObserversToCurrentScene();

        if (CurrentScene is CombatEncounterUI combatEncounterUI)
        {
            combatEncounterUI.SetEncounter((ICombatEncounter)Run.Instance.CurrentEncounter);
        }
    }

    private void RemoveObserversFromCurrentScene()
    {
        switch (CurrentScene)
        {
            case MapUI mapUI:
                mapUI.OnEncounterButtonPressed -= OnEncounterButtonPressed;
                return;
            case EncounterUI encounterUI:
                encounterUI.OnProceed -= OnProceed;
                return;
        }
    }
    
    private void AddObserversToCurrentScene()
    {
        switch (CurrentScene)
        {
            case MapUI mapUI:
                mapUI.OnEncounterButtonPressed += OnEncounterButtonPressed;
                return;
            case EncounterUI encounterUI:
                encounterUI.OnProceed += OnProceed;
                return;
        }
    }

    private void OnProceed()
    {
        GoToScene(_mapUIScene);
    }
}