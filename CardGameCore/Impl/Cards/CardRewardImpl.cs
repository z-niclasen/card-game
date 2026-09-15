using CardGameCore.Constants;
using CardGameCore.Framework.Cards;
using CardGameCore.Library;

namespace CardGameCore.Impl.Cards;

public class CardRewardImpl : ICardReward
{
    private readonly List<CardName> _pool;

    private readonly int _amountToGenerate;

    public CardRewardImpl(int amountToGenerate, List<CardName> pool)
    {
        _amountToGenerate = amountToGenerate;
        _pool = pool;
    }
    
    public IEnumerable<ICard> GenerateCards()
    {
        List<ICard> rewards = [];
        
        int poolSize = _pool.Count;
        Random rng = Run.Random;

        for (int i = 0; i < _amountToGenerate; i++)
        {
            int index = rng.Next(poolSize);
            CardName generatedCardName = _pool[index];
            rewards.Add(CardLibrary.InstantiateCardByName(generatedCardName));
        }

        return  rewards;
    }
}