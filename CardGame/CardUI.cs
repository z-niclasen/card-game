using CardGameCore.Constants;
using CardGameCore.Framework;
using CardGameCore.Framework.Cards;
using Godot;

namespace CardGame;

public delegate void CardSelectedDelegate(CardUI cardUI);

[Tool]
public partial class CardUI : Control
{
	private const string SceneUID = "uid://byss7fge32s1p";
	
	public event  CardSelectedDelegate OnCardSelected;

	public ICard Card
	{
		get;
		set
		{
			field = value;
			SetLabelsToCardValues();
			UpdateLabels();
		}
	}

	public int Width => (int)Size.X;
	
	public int Height => (int)Size.Y;
	
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

	public static PackedScene LoadScene()
	{
		return ResourceLoader.Load<PackedScene>(SceneUID);
	}

	public static CardUI InstantiateCard(ICard card)
	{
		var cardUI = LoadScene().Instantiate<CardUI>();
		cardUI.Card = card;
		return cardUI;
	}

	public override void _Ready()
	{
		_nameLabel = GetNode<Label>("%NameLabel");
		_costLabel = GetNode<Label>("%CostLabel");
		_descriptionLabel = GetNode<Label>("%DescriptionLabel");
		_rarityLabel = GetNode<Label>("%RarityLabel");
		_textureRect = GetNode<TextureRect>("%TextureRect");
		
		SetLabelsToCardValues();
		UpdateLabels();
		
		GuiInput += OnGuiInput;
		MouseEntered += OnMouseEntered;
		MouseExited += OnMouseExited;
	}

	public void Move(Vector2 newPosition)
	{
		Tween tween = CreateTween();
		tween.TweenProperty(this, "position", newPosition, 0.1f);
	}

	private void OnMouseExited()
	{
		_mouseOver = false;
	}

	private void OnMouseEntered()
	{
		_mouseOver = true;
	}

	private void OnGuiInput(InputEvent @event)
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
		GuiInput -= OnGuiInput;
		MouseEntered -= OnMouseEntered;
		MouseExited -= OnMouseExited;
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
		
		Texture2D texture = ResourceUtil.GetCardTexture(CardName);
		_textureRect?.Texture = texture;
	}
}