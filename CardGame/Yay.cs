using Godot;
using System;
using CardGame;
using CardGameCore.Constants;
using CardGameCore.Framework.Characters;
using CardGameCore.Impl;
using CardGameCore.Impl.CombatEncounter;
using CardGameCore.Library;
using CardGameCore.Library.Characters.PlayerCharacters;

public partial class Yay : Node2D
{
	[Export]
	private PlayerCombatCardCollectionUI CombatCardCollectionUI { get; set; }
	
	[Export]
	private CharacterUI CharacterUI { get; set; }
	
	private ICharacter _steve = new CharacterImpl(new SteveClass());
	
	[Export]
	private Button DrawCardButton { get; set; }

	private CombatCardCollection _collection;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_collection =
			new CombatCardCollection(SteveCards.StarterDeck, CombatCardCollection.ShuffleStrategy.Shuffle);

		CharacterUI.Character = _steve;
		CharacterUI.Visible = true;

		CharacterUI.Position = CharacterUI.Position with { X = CharacterUI.Position.X + 100, Y = CharacterUI.Position.Y + 100 }; 
		
		CombatCardCollectionUI.Collection = _collection;
		
		DrawCardButton.Pressed += DrawCardButtonOnPressed;
		DrawCardButton.Pressed += DealDamage;
	}

	private void DealDamage()
	{
		_steve.DecreaseResource(ResourceType.Health, 5);
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
