using System.Security.Principal;
using CardGameCore.Framework.Runs;
using CardGameCore.Impl;
using CardGameCore.Impl.Runs;

namespace CardGameCore;

public class Map
{
    public int LayerCount { get; } = 16;

    public const int LayerMaxWidth = 5;

    private Random _rng;
    
    public IEnumerable<MapEdge> Edges { get; }
    
    public IEnumerable<MapPosition> Positions { get; }
    
    public IEnumerable<IEnumerable<MapPosition>> PositionsByLayer { get; }
    
    public Map(Random rng)
    {
        _rng = rng;
        Edges = GenerateMap();
        
        HashSet<MapPosition> positions = new HashSet<MapPosition>();

        foreach (MapEdge edge in Edges)
        {
            positions.Add(edge.To);
            positions.Add(edge.From);
        }
        
        //positions.UnionWith(Edges.Select(x => x.From));
        //positions.UnionWith(Edges.Select(x => x.To));
        
        Positions  = positions;
        
        Console.WriteLine(positions.Count);
        
        PositionsByLayer = GeneratePositionsByLayer();

    }

    private List<MapEdge> GenerateMap()
    {
        List<MapEdge> edges = [];

        MapPosition bottom = new MapPosition(0, 2);
        MapPosition top = new MapPosition(LayerCount-1, 2);

        HashSet<MapPosition> previousLayer = [bottom];

        for (int layerIndex = 1; layerIndex < LayerCount - 1; layerIndex++)
        {
            List<MapEdge> layer = GenerateLayer(previousLayer);
            
            edges.AddRange(layer);
            previousLayer = new HashSet<MapPosition>(layer.Select(x => x.To));
            
        }

        foreach (MapPosition position in previousLayer)
        {
            edges.Add(new MapEdge(position, top));
        }

        return edges;
    }

    private List<MapEdge> GenerateLayer(HashSet<MapPosition> previousLayer)
    {
        List<MapEdge> layerEdges = [];
            
        List<MapPosition> shuffledPreviousLayer = previousLayer.Shuffle().ToList();
        
        int clampedToAdd = Math.Max(shuffledPreviousLayer.Count, 3);
            
        int amountOfEdgesToAdd =  _rng.Next(clampedToAdd, clampedToAdd * 2);

        for (int edgeNumber = 0; edgeNumber < amountOfEdgesToAdd; edgeNumber++)
        {
            MapPosition from;

            if (edgeNumber < shuffledPreviousLayer.Count)
            {
                from = shuffledPreviousLayer[edgeNumber];
            }
            else
            {
                from = shuffledPreviousLayer[_rng.Next(shuffledPreviousLayer.Count)];
            }
            
            MapPosition to = new MapPosition(from.Layer + 1, _rng.Next(LayerMaxWidth) );
            
            MapEdge edge = new MapEdge(from, to);

            edge = Nudge(edge, layerEdges);
            
            layerEdges.Add(edge);
        }
        
        return layerEdges;
    }

    private MapEdge Nudge(MapEdge edgeToNudge, IEnumerable<MapEdge> edges)
    {
        foreach (MapEdge edge in edges)
        {
            if (!edgeToNudge.ConflictsWith(edge)) continue;

            edgeToNudge = new MapEdge(edgeToNudge.From, edge.To);
        }
        
        return edgeToNudge;
    }
    
    public IEnumerable<IEnumerable<MapPosition>> GeneratePositionsByLayer()
    {
        List<List<MapPosition>> layers = [];
		
        for (int index =  0; index < LayerCount; index++)
        {
            layers.Add([]);
        }

        foreach (MapPosition position in Positions)
        {
            layers[position.Layer].Add(position);
        }

        foreach (List<MapPosition> layer in layers)
        {
            layer.Sort((x, y) => x.Index - y.Index);
        }

        return layers;
    }

    public readonly struct MapEdge : IEquatable<MapEdge>
    {
        public MapPosition From { get; }
        public MapPosition To { get; }

        public MapEdge(MapPosition from, MapPosition to)
        {
            From = from;
            To = to;
        }

        public bool ConflictsWith(MapEdge other)
        {
            if (From.Index <= other.From.Index && To.Index <= other.To.Index) return false;
            if (From.Index >= other.From.Index && To.Index >= other.To.Index) return false;

            return true;
        }

        public bool Equals(MapEdge other)
        {
            return From.Equals(other.From) && To.Equals(other.To);
        }

        public override bool Equals(object? obj)
        {
            return obj is MapEdge other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(From, To);
        }

        public override string ToString()
        {
            return $"{From} -> {To}";
        }
    }

    public readonly struct MapPosition : IEquatable<MapPosition>
    {
        public int Layer { get; }
        
        public int Index { get; }

        public MapPosition(int layer, int index)
        {
            Layer = layer;
            Index = index;
        }

        public bool Equals(MapPosition other)
        {
            return Layer == other.Layer && Index == other.Index;
        }

        public override bool Equals(object? obj)
        {
            return obj is MapPosition other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Layer, Index);
        }

        public override string ToString()
        {
            return $"({Layer}, {Index})";
        }
    }


}