using CardGameCore.Constants;
using CardGameCore.Framework.Characters;
using CardGameCore.Impl.Resources;
using Godot;

namespace CardGame;

public partial class CharacterUI : Node2D
{

	public ICharacter Character
	{
		get;
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

	public override void _Ready()
	{
		_healthBar = GetNode<TextureProgressBar>("%HealthBar");
		_healthLabel = GetNode<Label>("%HealthBar/%HealthAmountLabel");
		_sprite = GetNode<TextureRect>("%Sprite");
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
}