// File: DungeonGenerator.cs

using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    public Tilemap tilemap;  // Reference to the Tilemap
    public TileBase wallTile;  // Tile for the walls
    public TileBase floorTile;  // Tile for the floors

    public int mapWidth = 150;
    public int mapHeight = 150;

    void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        // Fill map with wall tiles
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), wallTile);
            }
        }

        // Carve out some rooms (for simplicity, we make a few rectangular rooms)
        for (int i = 0; i < 10; i++)
        {
            int roomWidth = Random.Range(4, 8);
            int roomHeight = Random.Range(4, 8);
            int startX = Random.Range(1, mapWidth - roomWidth - 1);
            int startY = Random.Range(1, mapHeight - roomHeight - 1);

            for (int x = startX; x < startX + roomWidth; x++)
            {
                for (int y = startY; y < startY + roomHeight; y++)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), floorTile);
                }
            }
        }
    }
}
