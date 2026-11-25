using CardGameCore.Framework;
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
    
    public IEncounter? CurrentEncounter { get; private set; }

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
        if (CurrentEncounter == null)
            return Map.CanMoveTo(CurrentMapPosition, newPosition);
        
        return CurrentEncounter.IsFinished && Map.CanMoveTo(CurrentMapPosition, newPosition);
    }

    public void MoveTo(CardGameCore.Map.MapPosition newPosition)
    {
        if (!CanMoveTo(newPosition))
            throw new ArgumentException($"Cannot move to {newPosition}");
        
        // Clean up old encounter
        CurrentEncounter?.OnRewardAcquired -= OnRewardAcquired;
        
        // Generate new encounter
        CombatEncounterImpl encounter = new(Player, new AiCharacterImpl(new SlimeClass()));
        
        // Move to new
        CurrentMapPosition = newPosition;
        CurrentEncounter = encounter;
        encounter.OnRewardAcquired += OnRewardAcquired;
    }
    
    private void OnRewardAcquired(IRewardPrimitive reward)
    {
        reward.Apply(this);
    }
}

