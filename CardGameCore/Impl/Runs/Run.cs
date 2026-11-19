using CardGameCore.Framework.Characters;
using CardGameCore.Library.Characters.PlayerCharacters;

namespace CardGameCore.Impl.Runs;

public class Run
{
    public static Run Instance
    {
        get
        {
            field ??= new Run();
            return field;
        }
    }

    public Random Random { get; }
    
    public RarityDistribution RarityDistribution { get; }

    public ICharacter Player { get; }

    public CardGameCore.Map Map { get; }

    private Run()
    {
        Player = new CharacterImpl(new SteveClass());
        Random =  new Random();
        RarityDistribution = new RarityDistribution();
        Map = new CardGameCore.Map(Random);
    }



    /*public void SelectNextNode(MapNode next)
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
    }*/




}

