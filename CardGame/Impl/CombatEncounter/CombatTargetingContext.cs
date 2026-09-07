using CardGame.Framework;
using CardGame.Framework.Characters;

namespace CardGame.Impl;

public class CombatTargetingContext(ICombatEncounter encounter, ICharacter target, ICharacter source)
{
    public ICombatEncounter Encounter { get; } = encounter;
    public ICharacter Target { get; } = target;
    public ICharacter Source { get; } = source;
}