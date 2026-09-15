using CardGameCore.Constants;
using CardGameCore.Framework.Cards;
using CardGameCore.Framework.Effects;
using CardGameCore.Impl.Effects;

namespace CardGameCore.Library.Characters.Steve.Cards;

public class StumbleCard : ICard
{
    public CardName Name => CardName.Stumble;
    
    public CharacterName Faction => CharacterName.Steve;
    
    public IEffect Effect => new EffectBuilder().NoneEffect().Build();
    
    public string Description => "Oops.";

    public Dictionary<ResourceType, int> Cost => new() { { ResourceType.Energy, 0 } };

    public Rarity Rarity => Rarity.Common;
}