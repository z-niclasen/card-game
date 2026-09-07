using CardGameCore.Framework;
using CardGameCore.Framework.Characters;

namespace CardGameCore.Impl.CombatEncounter;

public class CombatTargetingContext(ICombatEncounter encounter, ICharacter target, ICharacter source)
{
    public ICombatEncounter Encounter { get; } = encounter;
    public ICharacter Target { get; } = target;
    public ICharacter Source { get; } = source;
}