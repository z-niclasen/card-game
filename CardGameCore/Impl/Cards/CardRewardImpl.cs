using CardGameCore.Framework.Cards;
using CardGameCore.Library;

namespace CardGameCore.Impl.Cards;

public class CardRewardImpl : ICardReward
{
    private static List<ICard> _pool;
    
    public IEnumerable<ICard> GenerateCards()
    {
        List<ICard> rewards = [];
        
        rewards.Add(_pool[Run.Random.Next(_pool.Count)]);

        throw new NotImplementedException();
    }
}