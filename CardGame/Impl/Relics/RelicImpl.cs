using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Effects;

namespace CardGame.Impl.Relics;

public class RelicImpl : IRelic
{
    public RelicName Name { get; }
    
    public string Description { get; }
    
    public Rarity Rarity { get; }
    
    public IEnumerable<IEffectAdjustor> Offensive { get; }
    
    public IEnumerable<IEffectAdjustor> Defensive { get; }
    
    private RelicImpl(
        RelicName relicName, 
        string description, 
        IEnumerable<IEffectAdjustor> offensive, 
        IEnumerable<IEffectAdjustor> defensive, 
        Rarity rarity)
    {
        Name  = relicName;
        Description = description;
        Rarity = rarity;
        Offensive = offensive;
        Defensive = defensive;
    }

    public class Builder
    {
        private RelicName _name = RelicName.None;

        private string _description = "";

        private Rarity _rarity = Constants.Rarity.Common;

        private List<IEffectAdjustor> _offensive = [];

        private List<IEffectAdjustor> _defensive = [];

        public IRelic Build()
        {
            if (string.IsNullOrEmpty(_description))
                throw new InvalidOperationException($"Must set Description of relic.");
            
            if (_name == RelicName.None)
                throw new InvalidOperationException($"Must set Name of relic.");
            
            return new RelicImpl(_name, _description, _offensive, _defensive, _rarity);
        }

        public Builder Name(RelicName name)
        {
            _name = name;
            return this;
        }

        public Builder Description(string description)
        {
            _description = description;
            return this;
        }

        public Builder Rarity(Rarity rarity)
        {
            _rarity = rarity;
            return this;
        }

        public Builder Offensive(IEnumerable<IEffectAdjustor> offensive)
        {
            _offensive.AddRange(offensive);
            return this;
        }

        public Builder Offensive(IEffectAdjustor offensive)
        {
            _offensive.Add(offensive);
            return this;
        }

        public Builder Defensive(IEnumerable<IEffectAdjustor> defensive)
        {
            _defensive.AddRange(defensive);
            return this;
        }

        public Builder Defensive(IEffectAdjustor defensive)
        {
            _defensive.Add(defensive);
            return this;
        }
    }
}