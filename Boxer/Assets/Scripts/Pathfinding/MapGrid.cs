using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the grid.
/// </summary>
public class MapGrid
{
   private MapTile[][] _tiles;
   public readonly float TileSize;
   private int _x;
   private int _y;
   
   public Vector2Int Dimensions => new Vector2Int(_x, _y);

   public MapGrid(float tileSize)
   {
      TileSize = tileSize;
   }

   public void Initialize(int x, int y)
   {
      _tiles = new MapTile[x][];
      for (int i = 0; i < x; i++)
      {
         _tiles[i] = new MapTile[y];
      }

      _x = x;
      _y = y;
   }

   public void SetTile(MapTile tile, Vector2Int point)
   {
      if (IsWithinBounds(point))
      {
         _tiles[point.x][point.y] = tile;
      }
   }

   public MapTile GetTile(Vector2Int point)
   {
      if (IsWithinBounds(point))
      {
         return _tiles[point.x][point.y];
      }
      return null;
   }

   public List<MapTile> GetNeighbors(MapTile tile)
   {
      List<MapTile> neighbors = new List<MapTile>();
      Vector2Int gridPosition = WorldToGrid(tile.transform.position);
      if (GetTile(gridPosition) == tile)
      {
         foreach (Vector2Int direction in new List<Vector2Int>
            {Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left})
         {
            MapTile neighbor = GetTile(gridPosition + direction);
            if (neighbor != null)
            {
               neighbors.Add(neighbor);
            }
         }
      }
      return neighbors;
   }

   private bool IsWithinBounds(Vector2Int point)
   {
      return point.x >= 0 && point.x < _x && point.y >= 0 && point.y < _y;
   }

   public Vector2Int WorldToGrid(Vector3 worldPoint)
   {
      Vector2Int gridPoint = new Vector2Int(Mathf.RoundToInt((worldPoint.x) / TileSize + _x * 0.5f), 
         Mathf.RoundToInt((worldPoint.y) / TileSize + _y * 0.5f));
      return gridPoint;
   }
}
