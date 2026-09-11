using System;
using CardGameCore.Constants;
using CardGameCore.Framework;
using CardGameCore.Framework.Characters;
using CardGameCore.Framework.CombatEncounter;
using CardGameCore.Impl;
using CardGameCore.Impl.CombatEncounter;
using CardGameCore.Library.Characters.PlayerCharacters;
using CardGameCore.Test.Library;
using Godot;

namespace CardGame;

public partial class CombatEncounterUI : Node2D
{
	private ICharacter _steve;
	private IAiCharacter _slime;
	private ICombatEncounter _encounter;
	
	private CharacterUI _steveUI;
	private CharacterUI _slimeUI;
	private PlayerCombatCardCollectionUI _combatCollectionUI;

	private CardDisplay _cardDisplay;
	private RelicDisplay _relicDisplay;

	private Button _endTurnButton;
	private Button _playCardButton;

	private ICard _selectedCard;
	
	public override void _Ready()
	{
		ICharacterClass steveClass = new SteveClass();
		_steve = new CharacterImpl(steveClass);
        
		IAiCharacterClass cleverGreenSlimeClass = new TestingSlime(AiStrategy.PlayZero);
		_slime = new AiCharacterImpl(cleverGreenSlimeClass);

		_encounter = new CombatEncounterImpl(_steve, _slime);
		
		_steveUI = GetNode<CharacterUI>("%PlayerCharacter");
		_slimeUI = GetNode<CharacterUI>("%EnemyCharacter");
		_combatCollectionUI = GetNode<PlayerCombatCardCollectionUI>("%PlayerCardCollection");

		_steveUI.OnCharacterPressed += OnCharacterPressed;
		_slimeUI.OnCharacterPressed += OnCharacterPressed;
		
		_steveUI.Character = _steve;
		_slimeUI.Character = _slime;
		
		_cardDisplay = GetNode<CardDisplay>("%CardDisplay");
		_cardDisplay.OnDisplayedCardPressed += DeselectCard;
		
		_combatCollectionUI.Collection = _encounter.GetCombatCardCollectionOfCharacter(_steve);
		_combatCollectionUI.OnCardInHandSelected += SelectCardInHand;
		
		_endTurnButton = GetNode<Button>("%EndTurnButton");
		_playCardButton = GetNode<Button>("%PlayCard0Button");
		
		_relicDisplay = GetNode<RelicDisplay>("%RelicDisplay");
		_relicDisplay.RelicCollection = _steve.RelicCollection;
		
		AddObservers();
	}

	private void OnCharacterPressed(ICharacter character)
	{
		if (_selectedCard == null)
			return;
		
		if (!_encounter.InTurn.Tags.Contains(Tag.PlayerCharacter))
			return;

		try
		{
			_encounter.PlayCardFromHand(_steve, _selectedCard, character);
			_cardDisplay.HideCard();
			_selectedCard = null;
		}
		catch (Exception e)
		{
			// Ignore
		}
	}

	private void DeselectCard(ICard card)
	{
		_cardDisplay.HideCard();
		
		_combatCollectionUI.ShowCardInHand(_selectedCard);
		_selectedCard = null;
	}

	private void SelectCardInHand(ICard card)
	{
		if (_selectedCard != null)
			_combatCollectionUI.ShowCardInHand(_selectedCard);
		
		_selectedCard = card;
		_cardDisplay.DisplayCard(_selectedCard);
		_combatCollectionUI.HideCardInHand(_selectedCard);
	}

	public override void _ExitTree()
	{
		RemoveObservers();
		base._ExitTree();
	}

	private void EndTurnButtonOnPressed()
	{
		try
		{
			_encounter.EndTurn(_encounter.InTurn);
		}
		catch (Exception e)
		{
			// ignored
		} 
	}

	private void PlayCardButtonOnPressed()
	{
		if (!_encounter.InTurn.Tags.Contains(Tag.PlayerCharacter))
			return;

		ICard card = _encounter.GetCardFromHandAtIndex(_steve, 0);

		try
		{
			_encounter.PlayCardFromHand(_steve, card, _slime);
		}
		catch (Exception e)
		{
			// ignored
		}
	}

	private void AddObservers()
	{
		_endTurnButton.Pressed += EndTurnButtonOnPressed;
		_playCardButton.Pressed += PlayCardButtonOnPressed;
	}

	private void RemoveObservers()
	{
		_endTurnButton.Pressed -= EndTurnButtonOnPressed;
		_playCardButton.Pressed -= PlayCardButtonOnPressed;
	}
}