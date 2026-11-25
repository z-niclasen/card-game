using CardGameCore.Framework;

namespace CardGameCore.Impl;

public delegate void RewardSelectedDelegate(IRewardPrimitive reward);

public class Reward
{
    public event RewardSelectedDelegate? OnRewardSelected;

    public IEnumerable<IRewardPrimitive> Choices => _choices;
    private readonly List<IRewardPrimitive> _choices;
    
    public int MaximumPicks { get; }

    private int _amountSelected = 0;
    
    public Reward(IRewardPrimitive choice) : this([choice], 1) { }

    public Reward(IEnumerable<IRewardPrimitive> choices, int maximumPicks)
    {
        _choices = choices.ToList();
        MaximumPicks = maximumPicks;
    }

    public bool CanSelect(IRewardPrimitive reward)
    {
        if (!Choices.Contains(reward))
            return false;

        if (_amountSelected >= MaximumPicks)
            return false;

        return true;
    }

    public void SelectRewardPrimitive(IRewardPrimitive reward)
    {
        if (!Choices.Contains(reward))
            throw new ArgumentException("Illegal choice of reward primitive.");
        
        if (_amountSelected >= MaximumPicks)
            throw new InvalidOperationException("Cannot select more than maximum amount of rewards.");
        
        _choices.Remove(reward);
        _amountSelected++;
        OnRewardSelected?.Invoke(reward);
    }
}