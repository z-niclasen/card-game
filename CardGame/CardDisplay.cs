using CardGameCore.Framework;
using Godot;

namespace CardGame;

public delegate void DisplayedCardPressedDelegate(ICard card);

public partial class CardDisplay : Control
{
	public event  DisplayedCardPressedDelegate OnDisplayedCardPressed;
	
	private CardUI _cardUI;

	public override void _Ready()
	{
		_cardUI = GetNode<CardUI>("%CardUI");
		_cardUI.Visible = false;
		
		_cardUI.OnCardSelected += CardUIOnOnCardSelected;
	}

	private void CardUIOnOnCardSelected(CardUI cardUI)
	{
		if (!_cardUI.Visible)
			return;
		
		OnDisplayedCardPressed?.Invoke(cardUI.Card);
	}

	public void DisplayCard(ICard card)
	{
		_cardUI.Card = card;
		_cardUI.Visible = true;
	}

	public void HideCard()
	{
		_cardUI.Visible = false;
	}
}