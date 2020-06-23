using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Calculates passable paths between two points.
/// </summary>
public class MapPathfinder : MonoBehaviour
{
    private Map _map;

    public void Initialize(Map map)
    {
        _map = map;
    }

    public void CalculateAndMarkPath(Vector2 start, Vector2 end)
    {
        Vector2Int startPointGrid = _map.Grid.WorldToGrid(start);
        Vector2Int endPointGrid = _map.Grid.WorldToGrid(end);

        List<MapTile> path = CalculatePath(startPointGrid, endPointGrid);
        if (path.Count > 0) // found a path
        {
            _map.Painter.TrailTiles(path, Color.green);
        }
        else // failed to find a path
        {
            MapTile startTile = _map.Grid.GetTile(startPointGrid);
            MapTile endTile = _map.Grid.GetTile(endPointGrid);
            path.Add(startTile);
            path.Add(endTile);
            _map.Painter.TrailTiles(path, Color.red);
        }
    }

    private List<MapTile> CalculatePath(Vector2Int start, Vector2Int end)
    {
        List<MapTile> tiles = new List<MapTile>();
        MapTile startTile = _map.Grid.GetTile(start);
        MapTile endTile = _map.Grid.GetTile(end);
        if (startTile.Passable && endTile.Passable)
        {
            return AStarPath(startTile, endTile);
        }
        
        return tiles;
    }

    /// <summary>
    /// The classic A* pathfinding algorithm. Returns the list of tiles necessary to reach the goal.
    /// </summary>
    private List<MapTile> AStarPath(MapTile start, MapTile end)
    {
        List<MapTile> openSet = new List<MapTile> { start }; // sorted by fScore
        Dictionary<MapTile, MapTile> cameFrom = new Dictionary<MapTile, MapTile>();
        Dictionary<MapTile, float> gScore = new Dictionary<MapTile, float>(); // default value for all nodes should be infinite
        Dictionary<MapTile, float> fScore = new Dictionary<MapTile, float>(); // default value for all nodes should be infinite
        gScore[start] = 0;
        fScore[start] = 0;

        while (openSet.Count > 0)
        {
            MapTile current = openSet[0];
            if (current == end)
            {
                return ReconstructPath(cameFrom, current);
            }
            
            openSet.RemoveAt(0);
            foreach (MapTile neighbor in _map.Grid.GetNeighbors(current))
            {
                if (!neighbor.Passable)
                {
                    continue;
                }

                float tentativeGScore = gScore[current] + 1;
                
                if (!gScore.TryGetValue(neighbor, out float neighborGScore) || tentativeGScore < neighborGScore)
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + 1;
                    
                    if (openSet.Contains(neighbor))
                    {
                        openSet.Remove(neighbor);
                    }
                    
                    int index = 0;
                    for (int i = 0; i < openSet.Count; i++)
                    {
                        if (fScore[openSet[i]] < fScore[neighbor])
                        {
                            index++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    openSet.Insert(index, neighbor);
                }
            }
        }
        
        return new List<MapTile>();
    }
    
    /// <summary>
    /// A helper function for pathfinding, used to navigate back home.
    /// </summary>
    private List<MapTile> ReconstructPath(Dictionary<MapTile, MapTile> cameFrom, MapTile current)
    {
        List<MapTile> totalPath = new List<MapTile>();
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath.Insert(0, current);
        }

        return totalPath;
    }
}
