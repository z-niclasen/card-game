using System;
using CardGameCore.Constants;
using CardGameCore.Framework.Cards;
using CardGameCore.Framework.Characters;
using CardGameCore.Framework.CombatEncounter;
using Godot;

namespace CardGame;

public partial class CombatEncounterUI : EncounterUI
{
	public override event ProceedDelegate OnProceed;
	
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
	
	private Button _proceedButton;

	public void SetEncounter(ICombatEncounter encounter)
	{
		_encounter = encounter;
		Initialize();
	}

	private async void Initialize()
	{
		if (!IsNodeReady())
			await ToSignal(this, Node.SignalName.Ready);
		
		_steve = _encounter.Player;
		_steve.IncreaseResource(ResourceType.Armor, 1); // TODO: Remove;

		_slime = _encounter.Opponent;
		
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

		_encounter.OnRewardsAvailable += OnRewardsAvailable;
		
		_proceedButton = GetNode<Button>("%ProceedButton");
		_proceedButton.Disabled = true;
		_proceedButton.Visible = false;
		_proceedButton.Pressed += () => OnProceed?.Invoke();
		
		AddObservers();
	}

	private void OnRewardsAvailable()
	{
		_proceedButton.Visible = true;
		_proceedButton.Disabled = false;
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
		if (_selectedCard == null)
			return;
		
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
			DeselectCard(_selectedCard);
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