using CardGameCore.Constants;
using CardGameCore.Framework;
using CardGameCore.Impl;

namespace CardGameCore.Library;

public static class SteveCards
{
    public static Deck StarterDeck => new([
        Sword, Sword, Sword, Sword, Sword, Sword, Sword, Sword, Sword, Sword, Sword, Sword
    ]);
    
    public static ICard Sword => CardUtils.NewCard(CardName.Sword, CardUtils.DamageEffect(2), 1);
    
    public static ICard BigSword => CardUtils.NewCard(CardName.BigSword, CardUtils.DamageEffect(13), 2, Rarity.Uncommon);
    
    public static ICard Stumble => CardUtils.NewCard(CardName.Stumble, CardUtils.NoEffect(), 2, Rarity.Uncommon);
}