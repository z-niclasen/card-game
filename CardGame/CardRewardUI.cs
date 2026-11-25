using System.Collections.Generic;
using CardGameCore.Framework.Cards;
using Godot;

namespace CardGame;

public delegate void CardRewardSelectedDelegate(ICard card);

public partial class CardRewardUI : GridContainer
{
	private const string SceneUID = "uid://wale3pggtew2";
	
	public event CardRewardSelectedDelegate OnCardRewardSelected;
	
	public ICardReward CardReward
	{
		get;
		set
		{
			field = value;
			UpdateRewards();
		}
	}

	private readonly List<CardUI> _cards = new();

	public static PackedScene LoadScene()
	{
		return ResourceLoader.Load<PackedScene>(SceneUID);
	}

	public static CardRewardUI InstantiateCardRewardUI(ICardReward cardReward)
	{
		CardRewardUI cardRewardUI = LoadScene().Instantiate<CardRewardUI>();
		cardRewardUI.CardReward = cardReward;
		return cardRewardUI;
	}

	public override void _Ready()
	{
		if (CardReward != null)
			UpdateRewards();
	}

	public override void _ExitTree()
	{
		CleanUp();
		base._ExitTree();
	}

	private void UpdateRewards()
	{
		if (!IsNodeReady())
			return;
		
		CleanUp();
		
		if (CardReward == null)
			return;
		
		foreach (ICard card in CardReward.Rewards)
			AddCard(card);
	}

	private void AddCard(ICard card)
	{
		CardUI cardUI = CardUI.InstantiateCard(card);
		AddChild(cardUI);
		_cards.Add(cardUI);
		cardUI.OnCardSelected += OnCardSelected;
	}

	private void OnCardSelected(CardUI cardUI)
	{
		OnCardRewardSelected?.Invoke(cardUI.Card);
	}

	private void CleanUp()
	{
		foreach (CardUI oldCard in _cards)
		{
			oldCard.OnCardSelected -= OnCardSelected;
			oldCard.QueueFree();
		}
		_cards.Clear();
	}
}