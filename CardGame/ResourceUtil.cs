using System.Collections.Generic;
using CardGameCore.Constants;
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
    
    public static Texture2D GetCharacterTexture(CharacterName characterName)
    {
        return ResourceLoader.Load<Texture2D>(CharacterTextureMap[characterName]);
    }

    public static Texture2D GetRelicTexture(RelicName relicName)
    {
        return ResourceLoader.Load<Texture2D>(RelicTextureMap[relicName]);
    }
}