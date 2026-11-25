using CardGameCore.Impl.Runs;

namespace CardGameCore.Framework;

public interface IRewardPrimitive
{
    public void Apply(Run run);
}