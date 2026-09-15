using CardGameCore.Constants;
using CardGameCore.Framework.Cards;
using CardGameCore.Framework.Effects;
using CardGameCore.Impl.Effects;

namespace CardGameCore.Library.Characters.Steve.Cards;

public class BigSwordCard : ICard
{
    public CardName Name => CardName.BigSword;
    
    public CharacterName Faction => CharacterName.Steve;

    public IEffect Effect => new EffectBuilder()
        .DecreaseResource(ResourceType.Health, DamageAmount)
        .Build();

    public string Description => $"Deal {DamageAmount} damage";

    public Dictionary<ResourceType, int> Cost => new() { { ResourceType.Energy, EnergyCost } };

    public Rarity Rarity => Rarity.Rare;
    
    private const int DamageAmount = 13;
    private const int EnergyCost = 2;
}