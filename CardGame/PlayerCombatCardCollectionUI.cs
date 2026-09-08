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

	private PackedScene _cardUIScene;

	private readonly List<CardUI> _hand = [];

	public override void _Ready()
	{
		_cardUIScene = ResourceLoader.Load<PackedScene>("uid://byss7fge32s1p");
	}

	private void CollectionOnOnDrawCard(ICombatCardCollection collectionMutable, ICard card)
	{
		InstantiateCard(card);
	}

	private void CollectionOnOnDiscardCard(ICombatCardCollection collectionMutable, ICard card)
	{
		throw new NotImplementedException();
	}

	private void InstantiateCard(ICard card)
	{
		CardUI cardUI = _cardUIScene.Instantiate<CardUI>();
		AddChild(cardUI);
		
		cardUI.Card = card;
		_hand.Add(cardUI);
		
		DisplayHand();
	}
	
	private void DisplayHand()
	{
		if (_hand.Count == 0)
			return;

		float viewportWidth = GetViewport().GetVisibleRect().Size.X;
		float viewportHeight = GetViewport().GetVisibleRect().Size.Y;
		int midPoint = (int)(viewportWidth / 2);
		//float handWidth = (int)(0.8 * viewportWidth);
		
		int handCount = _hand.Count;
		int cardWidth = _hand[0].Width;
		int cardHeight = _hand[0].Height;
		int sep = (int)(0.2 * cardWidth);

		int handWidth = handCount * cardWidth + (handCount - 1) * sep;
		int startX = midPoint - handWidth / 2;

		for (int i = 0; i < handCount; i++)
		{
			CardUI card =  _hand[i];
			card.Visible = true;

			int newY = (int)(viewportHeight * 0.9 - (int)(cardHeight / 2));
			int newX = startX + (int)(0.5 * cardWidth) + i * (sep + cardWidth);
			
			card.Position = Position with { X =  newX, Y = newY };
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