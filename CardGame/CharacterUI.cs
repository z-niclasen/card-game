using CardGameCore.Constants;
using CardGameCore.Framework.Characters;
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
			RemoveObservers();
			_character = value;
			AddObservers();

			UpdateLabels();
			SetTexture();
		}
	}


	private ICharacter _character ;

	private TextureProgressBar _healthBar;

	private Label _healthLabel;

	private TextureRect _sprite;
	
	private bool _mouseOver = false;

	public override void _Ready()
	{
		_healthBar = GetNode<TextureProgressBar>("%HealthBar");
		_healthLabel = GetNode<Label>("%HealthBar/%HealthAmountLabel");
		_sprite = GetNode<TextureRect>("%Sprite");
		
		_sprite.MouseEntered += SpriteOnMouseEntered;
		_sprite.MouseExited += SpriteOnMouseExited;
		_sprite.GuiInput +=  SpriteOnGuiInput;
	}

	private void UpdateLabels()
	{
		int currentHealth =  _character.Health;
		int maxHealth = ((HealthResource) _character.GetResource(ResourceType.Health)).Max;
		
		string labelText = currentHealth + "/" + maxHealth;
		_healthBar.MaxValue = maxHealth;
		_healthBar.Value = currentHealth;
		_healthLabel.Text = labelText;
	}

	private void SetTexture()
	{
		_sprite.Texture = ResourceUtil.GetCharacterTexture(_character.Name);
	}

	private void AddObservers()
	{
		_character.OnIncreaseResource += CharacterOnOnIncreaseResource;
		_character.OnDecreaseResource += CharacterOnOnDecreaseResource;
	}

	private void CharacterOnOnDecreaseResource(ICharacter character, ResourceType type, int amount)
	{
		if  (type != ResourceType.Health)
		{
			return;
		}
		
		UpdateLabels();
	}

	private void CharacterOnOnIncreaseResource(ICharacter character, ResourceType type, int amount)
	{
		if  (type != ResourceType.Health)
		{
			return;
		}
		
		UpdateLabels();
	}

	private void RemoveObservers()
	{
		if (_character == null) return;
		_character.OnIncreaseResource -= CharacterOnOnIncreaseResource;
		_character.OnDecreaseResource -= CharacterOnOnDecreaseResource;
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