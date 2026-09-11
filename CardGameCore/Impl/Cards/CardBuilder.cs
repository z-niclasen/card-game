using CardGameCore.Constants;
using CardGameCore.Framework;
using CardGameCore.Framework.Effects;

namespace CardGameCore.Impl.Cards;

public class CardBuilder
{
    private CardName _name = CardName.NoName;
    
    private string _description = "";

    private IEffect _effect = null!;

    private readonly Dictionary<ResourceType, int> _cost = new Dictionary<ResourceType, int>();
    
    // Cards are common by default
    private Rarity _rarity = Constants.Rarity.Common;

    public CardBuilder Name(CardName name)
    {
        _name = name;
        return this;
    }

    public CardBuilder Effect(IEffect effect)
    {
        _effect = effect;
        return this;
    }

    public CardBuilder Description(string description)
    {
        _description = description;
        return this;
    }
    public CardBuilder Cost(ResourceType type, int cost)
    {
        
        if (_cost.ContainsKey(type))
        {
            _cost[type] += cost;
            return this;
        }
        
        _cost.Add(type, cost);
        return this;
    }

    public CardBuilder Rarity(Rarity rarity)
    {
        _rarity = rarity;
        return this;
    }

    public ICard Build()
    {
        if (_name == CardName.NoName)
            throw new InvalidOperationException("Cannot build card without name");
        if (_description == "")
            throw new InvalidOperationException("Cannot build card without description");
        if (_effect == null)
            throw new InvalidOperationException("Cannot build card without effect. Use NoneEffect for no effect");
        if (_cost.Count == 0)
            throw new InvalidOperationException("Cannot build card without cost. Use energy cost of 0 for free card");
        
        return new CardImpl(_name, _description, _effect, _cost, _rarity);

    }
}