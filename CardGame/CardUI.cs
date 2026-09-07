using CardGameCore.Constants;
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
	private int Cost { get; set; }
	
	[Export]
	private string Description { get; set; }
	
	[Export]
	private Rarity Rarity { get; set; }
	
	[Export]
	private Label NameLabel { get; set; }
	
	[Export]
	private Label CostLabel { get; set; }
	
	[Export]
	private Label DescriptionLabel { get; set; }
	
	[Export]
	private Label RarityLabel { get; set; }
	
	public override void _Ready()
	{
		//NameLabel.Text = SteveCards.BigSword.Name;
		GD.Print("Jørgen");
	}

	private void UpdateLabels()
	{
		NameLabel?.Text = CardName;
		CostLabel?.Text = Cost.ToString();
		DescriptionLabel?.Text = Description;
		RarityLabel?.Text = Rarity.ToString();
	}
}