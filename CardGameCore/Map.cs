using CardGameCore.Impl;
using CardGameCore.Impl.Runs;

namespace CardGameCore;

public class Map
{
    private const int LayerCount = 16;
    private const int LayerMaxWidth = 5;

    private Random _rng;
    
    private int[][] _map;
    private Dictionary<MapPosition, List<MapEdge>> _edges;
    
    public Map(Random rng)
    {
        _rng = rng;
        _map = GenerateMap();
        _edges = GenerateEdges(_map);
    }

    public IEnumerable<MapPosition> GetMapLayer(int layer)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(layer, LayerCount);
        ArgumentOutOfRangeException.ThrowIfNegative(layer);
        
        foreach (int layerIndex in _map[layer])
            yield return new MapPosition(layer, layerIndex);
    }

    public IEnumerable<IEnumerable<MapPosition>> GetMapLayers()
    {
        for (int layer = 0; layer < LayerCount; layer++)
            yield return GetMapLayer(layer);
    }

    public IEnumerable<MapEdge> GetMapEdges(MapPosition mapPosition)
    {
        foreach (MapEdge edge in _edges[mapPosition])
            Console.WriteLine(edge);
        return _edges[mapPosition];
    }

    public IEnumerable<MapEdge> GetAllMapEdges()
    {
        foreach (IEnumerable<MapPosition> layer in GetMapLayers())
        {
            foreach (MapPosition position in layer)
            {
                foreach (MapEdge edgesFromPosition in GetMapEdges(position))
                    yield return edgesFromPosition;
            }
        }
    }

    private Dictionary<MapPosition, List<MapEdge>> GenerateEdges(int[][] map)
    {
        Dictionary<MapPosition, List<MapEdge>> allEdges = new();
        
        // Populate with empty lists.
        for (int layer = 0; layer < LayerCount; layer++)
        {
            foreach (int indexInLayer in map[layer])
            {
                MapPosition mapPosition = new MapPosition(layer, indexInLayer);
                allEdges.Add(mapPosition, new List<MapEdge>());
            }
        }

        // Edges from bottom to all in second layer.
        MapPosition bottom = new(0, LayerMaxWidth / 2);
        foreach (int indexInSecondLayer in map[1])
        {
            MapPosition mapPositionInSecondRow = new(1, indexInSecondLayer);
            MapEdge mapEdgeToAdd = new MapEdge(bottom, mapPositionInSecondRow);
            allEdges[bottom].Add(mapEdgeToAdd);
        }
        
        // Edges from penultimate layer to top.
        MapPosition top = new(LayerCount - 1, LayerMaxWidth / 2);
        foreach (int indexInPenultimateLayer in map[LayerCount - 2])
        {
            MapPosition mapPositionInPenultimateLayer = new(LayerCount - 2, indexInPenultimateLayer);
            MapEdge mapEdgeToAdd = new MapEdge(mapPositionInPenultimateLayer, top);
            allEdges[top].Add(mapEdgeToAdd);
        }

        for (int layer = 1; layer < LayerCount - 2; layer++)
        {
            int layerSize = map[layer].Length;
            int[] layerIndicesShuffled = ((int[])map[layer].Clone()).Shuffle().ToArray();

            int edgesToGenerate = _rng.Next(layerSize, layerSize * 2);

            List<MapEdge> edgesCreated = [];

            for (int edgeNum = 0; edgeNum < edgesToGenerate; edgeNum++)
            {
                int indexFrom = 0;
                if (edgeNum < layerSize)
                    indexFrom = layerIndicesShuffled[edgeNum];
                else
                    indexFrom = layerIndicesShuffled[_rng.Next(layerSize)];

                int nextLayerCount = map[layer + 1].Length;
                int indexTo = map[layer + 1][_rng.Next(nextLayerCount)];
                
                MapPosition toMapPosition = new(layer + 1, indexTo);
                MapPosition fromMapPosition = new(layer, indexFrom);
                MapEdge mapEdgeToAdd = new(fromMapPosition, toMapPosition);

                foreach (var otherEdge in edgesCreated)
                {
                    if (!mapEdgeToAdd.ConflictsWith(otherEdge)) continue;

                    MapEdge nudgedMapEdge = new(fromMapPosition, otherEdge.To);
                    mapEdgeToAdd = nudgedMapEdge;
                }
                
                edgesCreated.Add(mapEdgeToAdd);
                allEdges[fromMapPosition].Add(mapEdgeToAdd);
            }
        }

        return allEdges;
    }
    
    private int[][] GenerateMap()
    {
        int[][] map = new int[16][];

        map[0] = new[] { LayerMaxWidth / 2 };
        map[LayerCount - 1] = new[] { LayerMaxWidth / 2 };

        for (int layer = 1; layer < LayerCount - 1; layer++)
            map[layer] = GenerateLayer();

        return map;
    }

    private int[] GenerateLayer()
    {
        List<int> indices = new List<int>(LayerMaxWidth);

        for (int i = 0; i < LayerMaxWidth; i++)
        {
            bool shouldAddIndex = _rng.NextDouble() > 0.4;
            if (!shouldAddIndex) 
                continue;
            
            indices.Add(i);
        }
        
        if (indices.Count == 0)
            indices.Add(LayerMaxWidth / 2);

        return indices.ToArray();
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