using CardGameCore.Constants;
using CardGameCore.Framework.Cards;
using CardGameCore.Framework.Relics;

namespace CardGameCore.Library;

public static class Library
{
    private static readonly Dictionary<CardName, Type> CardNameToType = new();
    private static readonly Dictionary<RelicName, Type> RelicNameToType = new();

    private static readonly Pool<CardName> CardPool = new();
    private static readonly Pool<RelicName> RelicPool = new();

    static Library()
    {
        InitializePools();
    }
    
    public static ICard InstantiateCardByName(CardName name)
    {
        if (Activator.CreateInstance(CardNameToType[name]) is not ICard card)
            throw new ArgumentException($"Card {name} does not exist in card library dictionary.");
        
        return card;
    }

    public static IEnumerable<CardName> GetCards(CharacterName characterName, Rarity rarity)
    {
        return CardPool.Get(characterName, rarity);
    }

    public static IEnumerable<RelicName> GetRelics(CharacterName characterName, Rarity rarity)
    {
        return RelicPool.Get(characterName, rarity);
    }

    private static void InitializePools()
    {
        // Cards
        foreach (Type type in GetCardImplementations())
        {
            if (Activator.CreateInstance(type) is not ICard card) 
                continue;
            
            AddCard(card, type);
        }
        
        // Relics
        foreach (Type type in GetRelicImplementations())
        {
            if (Activator.CreateInstance(type) is not IRelic relic) 
                continue;
            
            AddRelic(relic, type);
        }
    }

    private static void AddCard(ICard card, Type type)
    {
        CardNameToType.Add(card.Name, type);
        CardPool.Add(card.Faction, card.Rarity, card.Name);
    }

    private static void AddRelic(IRelic relic, Type type)
    {
        RelicNameToType.Add(relic.Name, type);
        RelicPool.Add(relic.Faction, relic.Rarity, relic.Name);
    }

    private static IEnumerable<Type> GetCardImplementations()
    {
        return GetImplementingTypes<ICard>();
    }
    
    private static IEnumerable<Type> GetRelicImplementations()
    {
        return GetImplementingTypes<IRelic>();
    }

    private static IEnumerable<Type> GetImplementingTypes<T>()
    {
        var type = typeof(T);
        
        // All classes that implements T (ICard, IRelic, etc)
        IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && p is { IsClass: true, IsAbstract: false });

        return types;
    }

    private class Pool<T>
    {
        private readonly Dictionary<CharacterName, Dictionary<Rarity, List<T>>> _nameToRarityToT = new();

        public Pool()
        {
            foreach (CharacterName characterName in Enum.GetValues<CharacterName>())
            {
                Dictionary<Rarity, List<T>> rarityToT = new();

                foreach (Rarity rarity in Enum.GetValues<Rarity>())
                {
                    rarityToT.Add(rarity, new List<T>());
                }
                
                _nameToRarityToT.Add(characterName, rarityToT);
            }
        }

        public void Add(CharacterName characterName, Rarity rarity, T name)
        {
            _nameToRarityToT[characterName][rarity].Add(name);
        }

        public IEnumerable<T> Get(CharacterName characterName, Rarity rarity)
        {
            return _nameToRarityToT[characterName][rarity];
        }
    }
}