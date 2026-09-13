using System;
using CardGameCore.Constants;
using CardGameCore.Framework.Characters;
using CardGameCore.Framework.Resources;
using CardGameCore.Impl.Resources;
using Godot;

namespace CardGame;

public delegate void CharacterPressedDelegate(ICharacter character);

public partial class CharacterUI : Node2D
{
	public event CharacterPressedDelegate OnCharacterPressed;

	public ICharacter Character
	{
		get => _character;
		set
		{
			RemoveCharacterObservers();
			_character = value;
			AddCharacterObservers();

			UpdateAllLabels();
			SetTexture();
		}
	}
	private ICharacter _character ;

	private TextureProgressBar _healthBar;
	private TextureProgressBar _energyBar;
	private TextureProgressBar _manaBar;
	private TextureRect _armorSprite;

	private Label _healthLabel;
	private Label _energyLabel;
	private Label _manaLabel;
	private Label _armorLabel;

	private TextureRect _sprite;
	
	private bool _mouseOver = false;

	public override void _Ready()
	{
		_healthBar = GetNode<TextureProgressBar>("%HealthBar");
		_energyBar = GetNode<TextureProgressBar>("%EnergyBar");
		_manaBar = GetNode<TextureProgressBar>("%ManaBar");
		_armorSprite = GetNode<TextureRect>("%ArmorSprite");
		
		_healthLabel = _healthBar.GetNode<Label>("%HealthAmountLabel");
		_energyLabel = _energyBar.GetNode<Label>("EnergyAmountLabel");
		_manaLabel = _manaBar.GetNode<Label>("%ManaAmountLabel");
		_armorLabel = _manaBar.GetNode<Label>("%ArmorAmountLabel");
		
		_sprite = GetNode<TextureRect>("%Sprite");
		
		AddCharacterObservers();
		AddInputObservers();
		UpdateAllLabels();
		SetTexture();
	}

	public override void _ExitTree()
	{
		RemoveCharacterObservers();
		RemoveInputObservers();
		
		base._ExitTree();
	}

	private void UpdateAllLabels()
	{
		if (_character == null)
			return;
		
		UpdateHealthLabel();
		UpdateEnergyLabel();
		UpdateManaLabel();
		UpdateArmorLabel();
	}
	
	private void UpdateLabel(ICharacter character, ResourceType type)
	{
		switch (type)
		{
			case ResourceType.Health:
				UpdateHealthLabel();
				return;
			case ResourceType.Energy:
				UpdateEnergyLabel();
				return;
			case ResourceType.Mana:
				UpdateManaLabel();
				return;
			case ResourceType.Armor:
				UpdateArmorLabel();
				return;
			default:
				throw new ArgumentOutOfRangeException(nameof(type), type, null);
		}
	}

	private void UpdateHealthLabel()
	{
		int currentHealth =  _character.Health;
		int maxHealth = ((HealthResource)_character.GetResource(ResourceType.Health)).Max;

		string labelText = $"{currentHealth} / {maxHealth}";
		_healthBar.MaxValue = maxHealth;
		_healthBar.Value = currentHealth;
		_healthLabel.Text = labelText;
	}

	private void UpdateEnergyLabel()
	{
		int currentEnergy = _character.Energy;
		int energyBaseline = ((EnergyResource)_character.GetResource(ResourceType.Energy)).Baseline;
		
		string labelText = $"{currentEnergy}";
		_energyBar.MaxValue = energyBaseline;
		_energyBar.Value = currentEnergy;
		_energyLabel.Text = labelText;
	}

	private void UpdateManaLabel()
	{
		if (!_character.HasResourceType(ResourceType.Mana))
		{
			_manaBar.Visible = false;
			return;
		}

		ManaResource manaResource = (ManaResource)_character.GetResource(ResourceType.Mana);
		int currentMana = manaResource.Amount;
		int manaGain = manaResource.ManaGainOnEncounterEnd;
		
		string labelText = $"{currentMana}";
		
		_manaBar.Visible = true;
		_manaBar.MaxValue = manaGain;
		_manaBar.Value = currentMana;
		_manaLabel.Text = labelText;
	}

	private void UpdateArmorLabel()
	{
		if (!_character.HasResourceType(ResourceType.Armor))
		{
			_armorSprite.Visible = false;
			return;
		}

		int currentArmor = _character.GetResourceAmount(ResourceType.Armor);
		
		string labelText = $"{currentArmor}";
		
		_armorSprite.Visible = true;
		_armorLabel.Text = labelText;
	}

	private void SetTexture()
	{
		if (_character == null)
			return;
		
		_sprite?.Texture = ResourceUtil.GetCharacterTexture(_character.Name);
	}

	private void AddCharacterObservers()
	{
		if (_character == null)
			return;
		
		_character.OnResourceChanged += UpdateLabel;
	}

	private void AddInputObservers()
	{
		_sprite.MouseEntered += SpriteOnMouseEntered;
		_sprite.MouseExited += SpriteOnMouseExited;
		_sprite.GuiInput += SpriteOnGuiInput;
	}

	private void RemoveCharacterObservers()
	{
		if (_character == null)
			return;
		
		_character.OnResourceChanged -= UpdateLabel;
	}

	private void RemoveInputObservers()
	{
		_sprite.MouseEntered -= SpriteOnMouseEntered;
		_sprite.MouseExited -= SpriteOnMouseExited;
		_sprite.GuiInput -= SpriteOnGuiInput;
	}
	
	private void SpriteOnMouseExited()
	{
		_mouseOver = false;
	}

	private void SpriteOnMouseEntered()
	{
		_mouseOver = true;
	}

	private void SpriteOnGuiInput(InputEvent @event)
	{
		if (@event is not InputEventMouseButton mouseEvent) 
			return;

		if (_mouseOver && mouseEvent.Pressed)
		{
			OnCharacterPressed?.Invoke(Character);
			
		}
	}
}