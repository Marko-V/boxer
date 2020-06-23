using UnityEngine;

/// <summary>
/// Spawns all grid tiles.
/// </summary>
public class MapGenerator : MonoBehaviour
{
   public Transform MapHolder;

   public Vector2Int Dimensions = new Vector2Int(160, 90);

   public MapTile Tile;
   
   private void Awake()
   {
      if (MapHolder == null) MapHolder = transform;
   }

   public MapGrid GenerateMap()
   {
      DestroyMap();
      
      MapGrid grid = new MapGrid(Tile.SpriteRenderer.sprite.bounds.size.x);
      grid.Initialize(Dimensions.x, Dimensions.y);

      Tile.gameObject.SetActive(true);
      for (int x = 0; x < Dimensions.x; x++)
      {
         for (int y = 0; y < Dimensions.y; y++)
         {
            MapTile newTile = Instantiate(Tile, MapHolder, false);
            newTile.transform.position = grid.TileSize * new Vector2(-Dimensions.x / 2 + x, -Dimensions.y / 2 + y);
            grid.SetTile(newTile, new Vector2Int(x, y));
         }
      }
      Tile.gameObject.SetActive(false);

      return grid;
   }

   public void DestroyMap()
   {
      foreach (Transform child in MapHolder)
      {
         Destroy(child.gameObject);
      }
   }
}
