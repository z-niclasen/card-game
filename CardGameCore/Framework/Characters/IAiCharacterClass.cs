using CardGameCore.Constants;

namespace CardGameCore.Framework.Characters;

public interface IAiCharacterClass : ICharacterClass
{
    public AiStrategy Strategy { get; }
}