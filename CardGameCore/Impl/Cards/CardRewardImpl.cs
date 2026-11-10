using CardGameCore.Constants;
using CardGameCore.Framework.Cards;

namespace CardGameCore.Impl.Cards;

public class CardRewardImpl : ICardReward
{
    private readonly RewardPool _rewardPool;
    private const int CardCount = 3;
    private readonly int _roomNumber;
    private readonly int _characterLuck;

    public IEnumerable<ICard> Rewards { get; }
    
    public CardRewardImpl(RewardPool rewardPool, int roomNumber, int characterLuck)
    {
        _rewardPool = rewardPool;
        _roomNumber = roomNumber;
        _characterLuck = characterLuck;

        Rewards = RollCards();
    }
    
    private IEnumerable<ICard> RollCards()
    {
        for (int i = 0; i < CardCount; i++)
        {
            Rarity cardRarity = Run.RarityDistribution.GetRarity(_roomNumber, _characterLuck);
            CardName cardName = _rewardPool.GetCard(cardRarity);
            yield return Library.Library.InstantiateCardByName(cardName);
        }
    }
}