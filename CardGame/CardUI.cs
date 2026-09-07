using CardGameCore.Constants;
using CardGameCore.Framework;
using Godot;

namespace CardGame;

[Tool]
public partial class CardUI : Node2D
{
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

	public int Width => BackgroundSprite.Texture.GetWidth();
	
	public int Height => BackgroundSprite.Texture.GetHeight();

	[Export]
	private Label NameLabel { get; set; }
	
	[Export]
	private Label CostLabel { get; set; }
	
	[Export]
	private Label DescriptionLabel { get; set; }
	
	[Export]
	private Label RarityLabel { get; set; }
	
	[Export]
	private Sprite2D BackgroundSprite { get; set; }
	
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

	public override void _Ready()
	{
		//NameLabel.Text = SteveCards.BigSword.Name;
		GD.Print("Jørgen");
	}

	private void SetLabelsToCardValues()
	{
		CardName = Card.Name;
		Cost = Card.Cost[ResourceType.Energy];
		Description = Card.Description;
		Rarity = Card.Rarity;
	}

	private void UpdateLabels()
	{
		NameLabel?.Text = CardName;
		CostLabel?.Text = Cost.ToString();
		DescriptionLabel?.Text = Description;
		RarityLabel?.Text = Rarity.ToString();
	}
}