using CardGame.Constants;
using CardGame.Framework;
using CardGame.Impl;

namespace CardGame.Library;

public static class SteveCards
{
    public static Deck StarterDeck => new([
        Sword, Sword, Sword, Sword, Sword, Sword, Sword, Sword, Sword, Sword, Sword, Sword
    ]);
    
    public static ICard Sword => CardUtils.NewCard("Sword", CardUtils.DamageEffect(2), 1);
    
    public static ICard BigSword => CardUtils.NewCard("Big Sword", CardUtils.DamageEffect(13), 2, Rarity.Uncommon);
    
    public static ICard Stumble => CardUtils.NewCard("Stumble", CardUtils.NoEffect(), 2, Rarity.Uncommon);
}