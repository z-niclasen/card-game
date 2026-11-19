using System.Collections.Generic;
using CardGameCore.Constants;
using CardGameCore.Impl;
using Godot;

namespace CardGame;

public static class ResourceUtil
{
    private static readonly Dictionary<CharacterName, string> CharacterTextureMap = new()
    {
        { CharacterName.Steve, "uid://b68j4prxg2due" },
        { CharacterName.GreenSlime, "uid://c4skvtwkt31mp" }
    };

    private static readonly Dictionary<RelicName, string> RelicTextureMap = new()
    {
        { RelicName.Shield, "uid://kbjm487so0jb" },
        { RelicName.Chalice, "uid://csjbuchs4epxk" }
    };

    private static readonly Dictionary<CardName, string> CardTextureMap = new()
    {
        { CardName.BigSword, "uid://b4dnsuvsvkeam" },
        { CardName.Sword, "uid://b4mjlgna0m81u" },
        { CardName.Stumble, "uid://cf16h5w8tog0u" },
    };
    
    private static readonly Dictionary<EncounterType, string> EncounterTypeTextureMap = new()
    {
        { EncounterType.Boss, "uid://c2noxpdpns5ib" },
        { EncounterType.Combat, "uid://c2noxpdpns5ib" },
        { EncounterType.Dialogue, "uid://c2noxpdpns5ib" },
        { EncounterType.Shop, "uid://c2noxpdpns5ib" },
    };
    
    public static Texture2D GetCharacterTexture(CharacterName characterName)
    {
        return ResourceLoader.Load<Texture2D>(CharacterTextureMap[characterName]);
    }

    public static Texture2D GetRelicTexture(RelicName relicName)
    {
        return ResourceLoader.Load<Texture2D>(RelicTextureMap[relicName]);
    }

    public static Texture2D GetCardTexture(CardName cardName)
    {
        return ResourceLoader.Load<Texture2D>(CardTextureMap[cardName]);
    }

    public static Texture2D GetEncounterTypeTexture(EncounterType encounterType)
    {
        return ResourceLoader.Load<Texture2D>(EncounterTypeTextureMap[encounterType]);
    }
}