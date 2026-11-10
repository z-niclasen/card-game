using CardGameCore.Constants;

namespace CardGameCore.Impl;

public class RarityDistribution
{
    private readonly Dictionary<Rarity, float> _dist = new()
    {
        { Rarity.Common, 100f },
        { Rarity.Uncommon, 9f },
        { Rarity.Rare, 0f },
        { Rarity.SuperDuperRareWowAwooga, 0f },
    };
    
    private readonly Dictionary<Rarity, float> _distMax = new()
    {
        { Rarity.Common, 100f },
        { Rarity.Uncommon, 50f },
        { Rarity.Rare, 15f },
        { Rarity.SuperDuperRareWowAwooga, 0.5f },
    };
    
    public Rarity GetRarity(int roomsCleared, int luck)
    {
        float uncommonChance = _dist[Rarity.Uncommon] + roomsCleared * 1.3f;
        float uncommonClamped = Math.Max(uncommonChance, _distMax[Rarity.Uncommon]);
        
        int roomsAfter10 = Math.Max(roomsCleared - 10, 0);
        float rareChance = _dist[Rarity.Rare] + roomsAfter10 * 0.5f;
        float rareClamped = Math.Max(rareChance, _distMax[Rarity.Rare]);

        float superChance = roomsCleared > 32 ? _distMax[Rarity.SuperDuperRareWowAwooga] : 0f;

        Rarity rarity = GetRandomRarity(uncommonClamped, rareClamped, superChance);
        
        float roll = (float)Run.Random.NextDouble() * 100;
        while (roll <= luck)
        {
            Rarity rarityReroll = GetRandomRarity(uncommonClamped, rareClamped, superChance);
            if (rarityReroll > rarity)
                rarity = rarityReroll;
            
            roll = (float)Run.Random.NextDouble() * 100;
        }

        return rarity;
    }

    private Rarity GetRandomRarity(float uncommonChance, float rareChance, float superChance)
    {
        Rarity highest = Rarity.Common;

        float roll = (float)Run.Random.NextDouble() * 100;

        if (roll <= uncommonChance)
            highest = Rarity.Uncommon;
        
        if (roll <= rareChance)
            highest = Rarity.Rare;
        
        if (roll <= superChance)
            highest = Rarity.SuperDuperRareWowAwooga;

        return highest;
    }
}