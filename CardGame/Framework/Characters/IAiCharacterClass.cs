using CardGame.Constants;

namespace CardGame.Framework.Characters;

public interface IAiCharacterClass : ICharacterClass
{
    public AiStrategy Strategy { get; }
}