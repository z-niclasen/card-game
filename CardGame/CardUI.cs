using CardGameCore.Constants;
using CardGameCore.Framework;
using Godot;

namespace CardGame;

[Tool]
public partial class CardUI : Node2D
{
	public ICard Card
	{
		get => _card;
		set
		{
			_card = value;
			SetLabelsToCardValues();
			UpdateLabels();
		} 
	}
	private ICard _card;
	
	public int Width => _backgroundSprite.Texture.GetWidth();
	
	public int Height => _backgroundSprite.Texture.GetHeight();
	
	[Export]
	private string CardName
	{
		get => _cardName;
		set
		{
			_cardName = value;
			UpdateLabels();
		}
	}
	private string _cardName = "";
	
	[Export]
	private int Cost 
	{ 
		get => _cost;
		set
		{
			_cost = value;
			UpdateLabels();
		} 
	}
	private int _cost = 0;

	[Export]
	private string Description
	{
		get => _description;
		set
		{
			_description = value;
			UpdateLabels();
		}
	}
	private string _description = "";

	[Export]
	private Rarity Rarity
	{
		get => _rarity;
		set
		{
			_rarity = value;
			UpdateLabels();
		}
	}
	private Rarity _rarity;

	private Label _nameLabel;

	private Label _costLabel;

	private Label _descriptionLabel;

	private Label _rarityLabel;

	private Sprite2D _backgroundSprite;

	public override void _Ready()
	{
		_nameLabel = GetNode<Label>("%NameLabel");
		_costLabel = GetNode<Label>("%CostLabel");
		_descriptionLabel = GetNode<Label>("%DescriptionLabel");
		_rarityLabel = GetNode<Label>("%RarityLabel");
		_backgroundSprite = GetNode<Sprite2D>("%BackgroundSprite");
		
		SetLabelsToCardValues();
		UpdateLabels();
	}

	private void SetLabelsToCardValues()
	{
		if (Card == null)
			return;
		
		CardName = Card.Name;
		Cost = Card.Cost[ResourceType.Energy];
		Description = Card.Description;
		Rarity = Card.Rarity;
	}

	private void UpdateLabels()
	{
		_nameLabel?.Text = CardName;
		_costLabel?.Text = Cost.ToString();
		_descriptionLabel?.Text = Description;
		_rarityLabel?.Text = Rarity.ToString();
	}
}