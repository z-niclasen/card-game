using CardGameCore.Constants;
using CardGameCore.Library;

namespace CardGameCore.Impl;

public class RewardPool
{
    private readonly Dictionary<Rarity, List<CardName>> _cardPool = new()
    {
        { Rarity.Common, [] },
        { Rarity.Uncommon, [] },
        { Rarity.Rare, [] },
        { Rarity.SuperDuperRareWowAwooga, [] }
    };

    private readonly Dictionary<Rarity, List<RelicName>> _relicPool = new()
    {
        { Rarity.Common, [] },
        { Rarity.Uncommon, [] },
        { Rarity.Rare, [] },
        { Rarity.SuperDuperRareWowAwooga, [] }
    };

    public RewardPool()
    {
        
    }

    public RewardPool(CharacterName characterName)
    {
        foreach (var (rarity, cardNames) in _cardPool)
            cardNames.AddRange(CardLibrary.GetCards(characterName, rarity));
    }

    public CardName GetCard(Rarity rarity)
    {
        return Get(rarity, _cardPool);
    }

    public RelicName GetRelic(Rarity rarity)
    {
        return Get(rarity, _relicPool);
    }

    private static T Get<T>(Rarity rarity, Dictionary<Rarity,  List<T>> pool)
    {
        Random rng = Run.Random;
        
        while (true)
        {
            int count = pool[rarity].Count;
            if (count > 0) return pool[rarity][rng.Next(count)];

            if (rarity == Rarity.Common) 
                throw new InvalidOperationException($"No {Rarity.Common} in pool of type {typeof(T)}.");

            rarity = rarity.Degrade();
        }
    }
}