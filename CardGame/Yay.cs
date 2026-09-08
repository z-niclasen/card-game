using Godot;
using System;
using CardGame;
using CardGameCore.Impl.CombatEncounter;
using CardGameCore.Library;

public partial class Yay : Node2D
{
	[Export]
	private PlayerCombatCardCollectionUI CombatCardCollectionUI { get; set; }
	
	[Export]
	private Button DrawCardButton { get; set; }

	private CombatCardCollectionImpl _collectionImpl;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_collectionImpl =
			new CombatCardCollectionImpl(SteveCards.StarterDeck, CombatCardCollectionImpl.ShuffleStrategy.Shuffle);
		
		CombatCardCollectionUI.CollectionImpl = _collectionImpl;
		
		DrawCardButton.Pressed += DrawCardButtonOnPressed;
	}

	private void DrawCardButtonOnPressed()
	{
		_collectionImpl.DrawCard();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
