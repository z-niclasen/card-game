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

	private Button _endTurnButton;
	private Button _playCardButton;
	
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
		
		_steveUI.Character = _steve;
		_slimeUI.Character = _slime;
		_combatCollectionUI.Collection = _encounter.GetCombatCardCollectionOfCharacter(_steve);
		
		_endTurnButton = GetNode<Button>("%EndTurnButton");
		_playCardButton = GetNode<Button>("%PlayCard0Button");
		
		AddObservers();
	}

	public override void _ExitTree()
	{
		RemoveObservers();
		base._ExitTree();
	}

	private void EndTurnButtonOnPressed()
	{
		_encounter.EndTurn(_encounter.InTurn);
	}

	private void PlayCardButtonOnPressed()
	{
		if (!_encounter.InTurn.Tags.Contains(Tag.PlayerCharacter))
			return;

		ICard card = _encounter.GetCardFromHandAtIndex(_steve, 0);
		_encounter.PlayCardFromHand(_steve, card, _slime);
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