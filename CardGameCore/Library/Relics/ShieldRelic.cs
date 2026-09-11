using CardGameCore.Constants;
using CardGameCore.Framework.Relics;
using CardGameCore.Impl.Relics;

namespace CardGameCore.Library.Relics;

public class ShieldRelic
{
    public static IRelic Relic = new RelicImpl.Builder()
        .Name(RelicName.Shield)
        .Offensive(new Kleenex())
        .Rarity(Rarity.Rare)
        .Description("Ser sej ud")
        .Build();
}