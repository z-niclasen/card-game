using CardGame.Framework;
using CardGame.Impl;

namespace CardGame.Library;

public static class GreenSlimeCards
{
    public static Deck StarterDeck => new([
        SlimeSpit, SlimeSpit
    ]);
    
    public static ICard SlimeSpit => CardUtils.NewCard("Slime Spit", CardUtils.DamageEffect(6), 1);
}