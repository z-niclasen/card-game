using CardGameCore.Constants;
using CardGameCore.Framework.Cards;

namespace CardGameCore.Library;

public static class CardLibrary
{
    private static readonly Dictionary<CardName, Type> NameToType = new();

    private static readonly Dictionary<CharacterName, Dictionary<Rarity, List<CardName>>> NameToRarityToCardName = new();

    static CardLibrary()
    {
        InitializeDict();
    }
    
    public static ICard InstantiateCardByName(CardName name)
    {
        if (Activator.CreateInstance(NameToType[name]) is not ICard card)
            throw new ArgumentException($"Card {name} does not exist in card library dictionary.");
        
        return card;
    }

    private static void InitializeDict()
    {
        foreach (Type type in GetCardImplementations())
        {
            if (Activator.CreateInstance(type) is not ICard card) 
                continue;
            
            AddCard(card, type);
        }
    }

    private static Dictionary<Rarity, List<CardName>> AddAndGetFaction(CharacterName faction)
    {
        if (NameToRarityToCardName.TryGetValue(faction, out Dictionary<Rarity, List<CardName>>? factionCards))
            return factionCards;

        Dictionary<Rarity, List<CardName>> empty = new() { };
        NameToRarityToCardName.Add(faction, empty);
        return empty;
    }

    private static List<CardName> AddAndGetCardsForRarity(Rarity rarity, Dictionary<Rarity, List<CardName>> faction)
    {
        if (faction.TryGetValue(rarity, out List<CardName>? cards))
            return cards;

        List<CardName> empty = new();
        faction.Add(rarity, empty);
        return empty;
    }

    private static void AddCard(ICard card, Type type)
    {
        NameToType.Add(card.Name, type);
        
        var rarityToCard = AddAndGetFaction(card.Faction);
        var cardNamesForRarity = AddAndGetCardsForRarity(card.Rarity, rarityToCard);
        cardNamesForRarity.Add(card.Name);
    }

    private static IEnumerable<Type> GetCardImplementations()
    {
        var type = typeof(ICard);
        
        IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && p is { IsClass: true, IsAbstract: false });

        return types;
    }
}