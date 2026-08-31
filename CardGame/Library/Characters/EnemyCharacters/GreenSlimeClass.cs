using CardGame.Constants;
using CardGame.Framework;
using CardGame.Framework.Characters;
using CardGame.Impl;
using CardGame.Impl.Resources;

namespace CardGame.Library.Characters.EnemyCharacters;

public class GreenSlimeClass : ICharacterClass
{
    public CharacterName Name => CharacterName.GreenSlime;

    public Dictionary<ResourceType, IResource> InitialResources => ResourceUtils.StandardResources(10, 1);

    public int InitialHandDrawCount => 1;

    public Deck StarterDeck => GreenSlimeCards.StarterDeck;
}