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

	private CombatCardCollection _collection;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_collection =
			new CombatCardCollection(SteveCards.StarterDeck, CombatCardCollection.ShuffleStrategy.Shuffle);
		
		CombatCardCollectionUI.Collection = _collection;
		
		DrawCardButton.Pressed += DrawCardButtonOnPressed;
	}

	private void DrawCardButtonOnPressed()
	{
		_collection.DrawCard();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
