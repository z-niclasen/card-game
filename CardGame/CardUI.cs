using CardGameCore.Constants;
using CardGameCore.Framework;
using Godot;

namespace CardGame;

public delegate void CardSelectedDelegate(CardUI cardUI);

[Tool]
public partial class CardUI : Node2D
{
	public event  CardSelectedDelegate OnCardSelected;
	
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
	
	public int Width => (int)_topContainer.CustomMinimumSize.X;
	
	public int Height => (int)_topContainer.CustomMinimumSize.Y;
	
	[Export]
	private CardName CardName
	{
		get => _cardName;
		set
		{
			_cardName = value;
			UpdateLabels();
		}
	}
	private CardName _cardName = CardName.NoName;

	private TextureRect _textureRect;
	
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
	
	private bool _mouseOver = false;

	private Label _nameLabel;

	private Label _costLabel;

	private Label _descriptionLabel;

	private Label _rarityLabel;

	private PanelContainer _topContainer;

	public override void _Ready()
	{
		_nameLabel = GetNode<Label>("%NameLabel");
		_costLabel = GetNode<Label>("%CostLabel");
		_descriptionLabel = GetNode<Label>("%DescriptionLabel");
		_rarityLabel = GetNode<Label>("%RarityLabel");
		_topContainer = GetNode<PanelContainer>("%TopContainer");
		_textureRect = GetNode<TextureRect>("%TextureRect");
		
		SetLabelsToCardValues();
		UpdateLabels();
		
		_topContainer.GuiInput += TopContainerOnGuiInput;
		_topContainer.MouseEntered += TopContainerOnMouseEntered;
		_topContainer.MouseExited += TopContainerOnMouseExited;
	}

	private void TopContainerOnMouseExited()
	{
		_mouseOver = false;
	}

	private void TopContainerOnMouseEntered()
	{
		_mouseOver = true;
	}

	private void TopContainerOnGuiInput(InputEvent @event)
	{
		if (@event is not InputEventMouseButton mouseEvent) 
			return;

		if (_mouseOver && mouseEvent.Pressed)
		{
			OnCardSelected?.Invoke(this);
		}
	}

	public override void _ExitTree()
	{
		_topContainer.GuiInput -= TopContainerOnGuiInput;
		_topContainer.MouseEntered -= TopContainerOnMouseEntered;
		_topContainer.MouseExited -= TopContainerOnMouseExited;
		base._ExitTree();
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
		_nameLabel?.Text = CardName.ToString();
		_costLabel?.Text = Cost.ToString();
		_descriptionLabel?.Text = Description;
		_rarityLabel?.Text = Rarity.ToString();

		if (CardName == CardName.NoName) return;
		
		GD.Print($"Fetching texture for card {CardName}");
		
		Texture2D texture = ResourceUtil.GetCardTexture(CardName);
		_textureRect.Texture = texture;


	}
}