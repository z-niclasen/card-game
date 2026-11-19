namespace CardGameCore.Framework.Run;

public delegate void OnRewardsSelected(List<IReward> rewards);
public interface IEncounter
{

    public event OnRewardsSelected? OnRewardsSelected;

}

