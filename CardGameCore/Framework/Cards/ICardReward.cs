namespace CardGameCore.Framework.Cards;

public interface ICardReward
{
    public IEnumerable<ICard> GenerateCards();
}