using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// "Paints" on the grid using tile properties and trails.
/// </summary>
public class MapPainter : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trailRenderer;
    public int BrushSize = 1;
    private MapGrid _grid;
    public void Initialize(Map map)
    {
        _grid = map.Grid;
    }

    public void TurnImpassable(Vector2 point)
    {
        Vector2Int gridPoint = _grid.WorldToGrid(point);
        foreach (MapTile tile in GetAllMapTargets(gridPoint))
        {
            tile.Passable = false;
        }
    }

    public void ResetLevel()
    {
        Vector2Int dimensions = _grid.Dimensions;
        for (int i = 0; i < dimensions.x; i++)
        {
            for (int j = 0; j < dimensions.y; j++)
            {
                MapTile tile = _grid.GetTile(new Vector2Int(i, j));
                if (!tile.Passable)
                {
                    tile.Passable = true;
                }
            }
        }
    }

    public void InvertLevel()
    {
        Vector2Int dimensions = _grid.Dimensions;
        for (int i = 0; i < dimensions.x; i++)
        {
            for (int j = 0; j < dimensions.y; j++)
            {
                MapTile tile = _grid.GetTile(new Vector2Int(i, j));
                tile.Passable = !tile.Passable;
            }
        }
    }

    private List<MapTile> GetAllMapTargets(Vector2Int gridPoint)
    {
        List<MapTile> tiles = new List<MapTile>();
        for (int i = -BrushSize + 1; i < BrushSize; i++)
        {
            for (int j = -BrushSize + 1; j < BrushSize; j++)
            {
                tiles.Add(_grid.GetTile(gridPoint + new Vector2Int(i, j)));
            }
        }

        return tiles;
    }

    public void TrailTiles(List<MapTile> tiles, Color color)
    {
        if (tiles.Count == 0)
        {
            return;
        }
        
        _trailRenderer.transform.position = tiles[0].transform.position; // place renderer in final position
        _trailRenderer.Clear();
        _trailRenderer.startColor = color;
        for (int i = tiles.Count - 1; i >= 0; i--)
        {
            _trailRenderer.AddPosition(tiles[i].transform.position);
        }
    }
}
