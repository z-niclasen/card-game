namespace CardGameCore.Framework.Cards;

public interface ICardReward
{
    public IEnumerable<ICard> Rewards { get; }
}