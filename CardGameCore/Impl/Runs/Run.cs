using CardGameCore.Framework.Characters;
using CardGameCore.Framework.Runs;
using CardGameCore.Impl.CombatEncounter;
using CardGameCore.Library.Characters.PlayerCharacters;
using CardGameCore.Library.Enemies.Slime;

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
    
    public CardGameCore.Map.MapPosition CurrentMapPosition { get; private set; }
    
    public IEncounter CurrentEncounter { get; private set; }

    private Run()
    {
        Player = new CharacterImpl(new SteveClass());
        Random =  new Random();
        RarityDistribution = new RarityDistribution();
        Map = new CardGameCore.Map(Random);
        CurrentMapPosition = Map.RootPosition;
    }

    public bool CanMoveTo(CardGameCore.Map.MapPosition newPosition)
    {
        return Map.CanMoveTo(CurrentMapPosition, newPosition);
    }

    public void MoveTo(CardGameCore.Map.MapPosition newPosition)
    {
        if (!CanMoveTo(newPosition))
            throw new ArgumentException($"Cannot move to {newPosition}");
        
        CurrentMapPosition = newPosition;

        CombatEncounterImpl encounter = new(new CharacterImpl(new SteveClass()), new AiCharacterImpl(new SlimeClass()));
        CurrentEncounter = encounter;
        encounter.OnRewardsSelected += ApplyRewards;
    }
    
    private void ApplyRewards(IEnumerable<IReward> rewards)
    {
        foreach (var reward in rewards)
        {
            reward.Apply(this);
        }

        CurrentEncounter.OnRewardsSelected -= ApplyRewards;
    }
}

