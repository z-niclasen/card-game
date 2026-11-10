namespace CardGameCore.Framework.Relics;

public interface IRelicReward
{
    public IEnumerable<IRelic> Rewards { get; }
}