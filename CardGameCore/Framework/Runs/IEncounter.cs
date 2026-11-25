using CardGameCore.Impl;

namespace CardGameCore.Framework.Runs;

public delegate void RewardAcquiredDelegate(IRewardPrimitive reward);

public delegate void FinishedDelegate();

public interface IEncounter
{
    public event RewardAcquiredDelegate OnRewardAcquired;
    
    public bool IsFinished { get; }
    
    public IEnumerable<Reward> Rewards { get; }
}

