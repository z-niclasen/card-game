namespace CardGameCore.Framework.Run;

public interface IReward
{
    public void Apply(Impl.Run run);
}