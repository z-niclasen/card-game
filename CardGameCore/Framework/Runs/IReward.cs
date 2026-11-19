using CardGameCore.Impl.Runs;

namespace CardGameCore.Framework.Runs;

public interface IReward
{
    public void Apply(Run run);
}