using CardGameCore.Constants;
using CardGameCore.Framework.Effects;

namespace CardGameCore.Framework.Cards;

public interface ICard
{
    public CardName Name { get; }
    
    public CharacterName Faction { get; }
    
    public IEffect Effect { get; }
    
    public string Description { get; }
    
    public Dictionary<ResourceType, int> Cost { get; }
    
    public Rarity Rarity { get; }
}