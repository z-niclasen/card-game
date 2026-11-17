using System.Reflection.Metadata.Ecma335;
using CardGameCore.Exceptions;
using CardGameCore.Framework.Characters;
using CardGameCore.Framework.CombatEncounter;
using CardGameCore.Framework.Run;
using CardGameCore.Impl.CombatEncounter;
using CardGameCore.Library.Enemies.Slime;

namespace CardGameCore.Impl;

public class Map
{

    private const int RoomsPerAct = 15;
    private const int Acts = 3;
    private const int Width = 5;
    
    private MapNode Root { get; }
    
    public MapNode Current { get; private set; }

    private readonly Run Run;

    public Map(Run run)
    {
        Run = run;

        Root = GenerateMap();
        Current = Root;
    }

    // TODO: Implement actual logic for types
    private MapNode GenerateMap()
    {
        MapNode root =  new MapNode(EncounterType.Combat, [], 0);
        
        
        return root;
    }
    


    public void SelectNextNode(MapNode next)
    {
        if (!Current.Children.Contains(next))
        {
            throw new IllegalNodeSelectionException("Cannot select node not child of current node");
        }
        
        Current = next;
        
        Current.Encounter = Current.Type switch
        {
            EncounterType.Combat => new CombatEncounterImpl(Run.Player, new AiCharacterImpl(new SlimeClass())),
            EncounterType.Dialogue or EncounterType.Shop or EncounterType.Elite or EncounterType.Boss => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException()
        };

        

    }
}

public class MapNode
{
    
    public EncounterType Type { get; }
    public IEncounter Encounter { get;  set; }
    
    public IEnumerable<MapNode> Children { get; set; }
    
    public int Level { get; set; }

    public MapNode(EncounterType type, IEnumerable<MapNode> children, int level)
    {
        Children = children;
        Level = level;
        Type = type;
    }
}

public enum EncounterType
{
    Combat, Dialogue, Shop, Elite, Boss,
}