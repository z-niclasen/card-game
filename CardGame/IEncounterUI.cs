using Godot;

namespace CardGame;

public delegate void ProceedDelegate();

public abstract partial class EncounterUI : Node
{
    public abstract event ProceedDelegate OnProceed;
}