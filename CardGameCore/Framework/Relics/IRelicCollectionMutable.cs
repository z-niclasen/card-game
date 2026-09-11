namespace CardGameCore.Framework.Relics;

public interface IRelicCollectionMutable : IRelicCollection
{
    public void AddRelic(IRelic relic);

    public void AddRelics(IEnumerable<IRelic> startingRelics);

    public void RemoveRelic(IRelic relic);
}