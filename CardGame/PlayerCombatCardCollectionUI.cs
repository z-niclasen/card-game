using System;
using System.Collections.Generic;
using CardGameCore.Framework;
using CardGameCore.Framework.CombatEncounter;
using CardGameCore.Impl.CombatEncounter;
using Godot;

namespace CardGame;

public partial class PlayerCombatCardCollectionUI : Node2D
{
	public ICombatCardCollection CollectionImpl
	{
		get => _collection;
		set
		{
			RemoveObservers();
			_collection = value;
			AddObservers();
		}
	}
	private ICombatCardCollection _collection;

	[Export]
	private int HandOffset { get; set; } = 200;

	[Export]
	private int CardSeparator { get; set; } = 30;

	private PackedScene _cardUIScene;

	private readonly List<CardUI> _hand = [];
	private readonly Dictionary<ICard, CardUI> _cardMap = new();
	
	private Button _discardPileButton;
	private Button _drawPileButton;
	private Control _handContainer;

	public override void _Ready()
	{
		_cardUIScene = ResourceLoader.Load<PackedScene>("uid://byss7fge32s1p");
		
		_discardPileButton = GetNode<Button>("%DiscardPileButton");
		_drawPileButton = GetNode<Button>("%DrawPileButton");
		_handContainer = GetNode<Control>("%HandContainer");
	}

	private void CollectionOnOnDrawCard(ICombatCardCollection collectionMutable, ICard card)
	{
		InstantiateCard(card);
	}

	private void CollectionOnOnDiscardCard(ICombatCardCollection collectionMutable, ICard card)
	{
		DisposeCard(card);
	}

	private void InstantiateCard(ICard card)
	{
		CardUI cardUI = _cardUIScene.Instantiate<CardUI>();
		AddChild(cardUI);
		
		cardUI.Card = card;
		_hand.Add(cardUI);
		_cardMap.Add(card, cardUI);
		
		DisplayHand();
	}

	private void DisposeCard(ICard card)
	{
		CardUI cardUI =  _cardMap[card];
		
		cardUI.QueueFree();
		
		_hand.Remove(cardUI);
		_cardMap.Remove(card);
	}
	
	private void DisplayHand()
	{
		int handCount = _hand.Count;
		
		if (handCount == 0)
			return;
		
		//float viewportWidth = GetViewport().GetVisibleRect().Size.X;
		//float viewportHeight = GetViewport().GetVisibleRect().Size.Y;
		
		float totalHandWidth = _handContainer.Size.X;
		float handHorizontalMidPoint = _handContainer.Position.X + totalHandWidth / 2;
		float handVerticalMidPoint = _handContainer.Position.Y + _handContainer.Size.Y / 2;

		int cardHeight = _hand[0].Height;
		int cardWidth = _hand[0].Width;
		int totalCardWidths = cardWidth * handCount + CardSeparator * (handCount - 1);
		
		int actualCardWidth = (totalCardWidths <= totalHandWidth) ? totalCardWidths / handCount 
			: (int)(totalHandWidth / handCount);
		
		bool evenCards = handCount % 2 == 0;

		int startX = evenCards ? (int)(handHorizontalMidPoint - ((handCount / 2) * actualCardWidth) + (int)(actualCardWidth * 0.5f))
			: (int)(handHorizontalMidPoint - ((float)handCount - 1) / 2 * actualCardWidth);

		for (int i = 0; i < handCount; i++)
		{
			CardUI card = _hand[i];
			card.Visible = true;

			float newY = handVerticalMidPoint;
			float newX = startX + i * actualCardWidth;

			card.Position = Position with { X = newX, Y = newY };
		}
	}

	private void AddObservers()
	{
		_collection?.OnDrawCard += CollectionOnOnDrawCard;
		_collection?.OnDiscardCard += CollectionOnOnDiscardCard;
	}

	private void RemoveObservers()
	{
		_collection?.OnDrawCard -= CollectionOnOnDrawCard;
		_collection?.OnDiscardCard -= CollectionOnOnDiscardCard;
	}
}