using System;
using System.Collections.Generic;
using CardGameCore.Framework;
using CardGameCore.Impl.CombatEncounter;
using Godot;

namespace CardGame;

public partial class PlayerCombatCardCollectionUI : Node2D
{
	public CombatCardCollection Collection
	{
		get => _collection;
		set
		{
			RemoveObservers();
			_collection = value;
			AddObservers();
		}
	}
	
	private CombatCardCollection _collection;
	
	[Export]
	private PackedScene CardUIScene { get; set; }

	private List<CardUI> _hand = [];

	private void CollectionOnOnDrawCard(CombatCardCollection collection, ICard card)
	{
		InstantiateCard(card);
	}

	private void CollectionOnOnDiscardCard(CombatCardCollection collection, ICard card)
	{
		throw new NotImplementedException();
	}

	private void InstantiateCard(ICard card)
	{
		CardUI cardUI = CardUIScene.Instantiate<CardUI>();

		cardUI.Card = card;
		AddChild(cardUI);
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