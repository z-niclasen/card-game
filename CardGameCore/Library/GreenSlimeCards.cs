using CardGameCore.Constants;
using CardGameCore.Framework;
using CardGameCore.Impl;

namespace CardGameCore.Library;

public static class GreenSlimeCards
{
    public static Deck StarterDeck => new([
        SlimeSpit, SlimeSpit
    ]);
    
    public static ICard SlimeSpit => CardUtils.NewCard(CardName.SlimeSpit, CardUtils.DamageEffect(6), 1);
}