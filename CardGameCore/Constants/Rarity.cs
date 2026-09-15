namespace CardGameCore.Constants;

public enum Rarity
{
    Common = 0, Uncommon = 1, Rare = 2, SuperDuperRareWowAwooga = 3
}

public static class RarityExtensions
{
    extension(Rarity rarity)
    {
        public Rarity Upgrade()
        {
            return rarity switch
            {
                Rarity.Common => Rarity.Uncommon,
                Rarity.Uncommon => Rarity.Rare,
                Rarity.Rare => Rarity.SuperDuperRareWowAwooga,
                Rarity.SuperDuperRareWowAwooga => throw new ArgumentException($"Cannot upgrade {Rarity.SuperDuperRareWowAwooga} rarity."),
                _ => throw new ArgumentOutOfRangeException(nameof(rarity), rarity, null)
            };
        }

        public Rarity Degrade()
        {
            return rarity switch
            {
                Rarity.Common => throw new ArgumentException($"Cannot degrade {Rarity.Common} rarity."),
                Rarity.Uncommon => Rarity.Common,
                Rarity.Rare => Rarity.Uncommon,
                Rarity.SuperDuperRareWowAwooga => Rarity.Rare,
                _ => throw new ArgumentOutOfRangeException(nameof(rarity), rarity, null)
            };
        }
    }
}