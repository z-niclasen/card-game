using System.Text;
using CardGameCore.Exceptions;
using CardGameCore.Framework.Characters;
using CardGameCore.Framework.Run;

namespace CardGameCore.Impl;

public class Run
{
    public static readonly Random Random = new();
    
    public static readonly RarityDistribution RarityDistribution = new();

    public ICharacter Player { get; }
    
    private Map Map { get; }

    public void SelectNextNode(MapNode next)
    {
        Map.SelectNextNode(next);
        Map.Current.Encounter.OnRewardsSelected += ApplyRewards;
    }

    private void ApplyRewards(IEnumerable<IReward> rewards)
    {
        foreach (var reward in rewards)
        {
            reward.Apply(this);
        }
        
        Map.Current.Encounter.OnRewardsSelected -= ApplyRewards;
    }
    
    
    

}

