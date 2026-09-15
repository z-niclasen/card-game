using CardGameCore.Constants;
using CardGameCore.Library;

namespace CardGameCore.Impl;

public class RewardPool
{
    private readonly Dictionary<Rarity, List<CardName>> _pool = new()
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
        foreach (var (rarity, cardNames) in _pool)
            cardNames.AddRange(CardLibrary.GetCards(characterName, rarity));
    }

    public CardName Get(Rarity rarity)
    {
        Random rng = Run.Random;
        
        while (true)
        {
            int count = _pool[rarity].Count;
            if (count > 0) return _pool[rarity][rng.Next(count)];

            if (rarity == Rarity.Common) 
                throw new InvalidOperationException($"No {Rarity.Common} cards in RewardPool.");

            rarity = rarity.Degrade();
        }
    }
}