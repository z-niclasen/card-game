using CardGameCore.Framework.Runs;

namespace CardGameCore.Framework.Runs;

public delegate void OnRewardsSelected(List<IReward> rewards);
public interface IEncounter
{

    public event OnRewardsSelected? OnRewardsSelected;

}

