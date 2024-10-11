// File: DungeonGenerator.cs

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    public int mapWidth = 150;
    public int mapHeight = 150;
    public Tilemap tilemap;
    public TileBase tileBarrier;
    public TileBase tileFloor;
    public TileBase tileStone;

    private List<RectInt> rooms = new List<RectInt>();
    private List<RectInt> rooms2;
    private const int numRooms = 5;
    private const int localRange = 35;
    private const int minRoomSize = 4;
    private const int maxRoomSize = 8;

    void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        FillMapWithStone();
        // 10x10 room in center for wizard's house/lab
        rooms.Add(new RectInt(mapWidth / 2 - 5, mapHeight / 2 - 5, 10, 10));
        SetRoomTiles(rooms[0], tileFloor);
        CarveRooms();
        rooms2 = new List<RectInt>(rooms); // Duplicate rooms
        BuildConnections();
    }

    void FillMapWithStone()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (x == 0 || y == 0 || x == mapWidth - 1 || y == mapHeight - 1)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileBarrier);
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileStone);
                }
            }
        }
    }

    RectInt RandomRoom()
    {
        int width = Random.Range(minRoomSize, maxRoomSize + 1);
        int height = Random.Range(minRoomSize, maxRoomSize + 1);
        int x = Random.Range(1, mapWidth - width - 1);
        int y = Random.Range(1, mapHeight - height - 1);
        return new RectInt(x, y, width, height);
    }

    bool RoomOverlaps(RectInt room)
    {
        foreach (RectInt existingRoom in rooms)
        {
            if (existingRoom.Overlaps(room))
            {
                return true;
            }
        }
        return false;
    }

    void CarveRooms()
    {
        for (int i = 0; i < numRooms; i++)
        {
            RectInt room = RandomRoom();
            while (i > 0 && RoomOverlaps(room))
            {
                room = RandomRoom();
            }

            rooms.Add(room);
            SetRoomTiles(room, tileFloor);
        }
    }

    void SetRoomTiles(RectInt room, TileBase tile)
    {
        for (int x = room.xMin; x < room.xMax; x++)
        {
            for (int y = room.yMin; y < room.yMax; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }

    void BuildConnections()
    {
        while (rooms.Count > 1)
        {
            RectInt room1 = rooms[0];
            Vector2 source = room1.center;
            int closestIndex = -1;
            float closestDist = localRange;

            for (int i = 1; i < rooms.Count; i++)
            {
                float dist = Vector2.Distance(source, rooms[i].center);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestIndex = i;
                }
            }

            if (closestIndex != -1)
            {
                ConnectRooms(room1, rooms[closestIndex]);
                rooms.RemoveAt(closestIndex);
            }
            rooms.RemoveAt(0);
        }
    }

    void ConnectRooms(RectInt room1, RectInt room2)
    {
        Vector2Int start = new Vector2Int(
            Random.Range(room1.xMin, room1.xMax - 1),
            Random.Range(room1.yMin, room1.yMax - 1)
        );
        Vector2Int end = new Vector2Int(
            Random.Range(room2.xMin, room2.xMax - 1),
            Random.Range(room2.yMin, room2.yMax - 1)
        );

        Vector2Int current = start;
        while (current != end)
        {
            if (Random.value < 0.5f && current.x != end.x)
            {
                current.x += (end.x > current.x) ? 1 : -1;
            }
            else if (current.y != end.y)
            {
                current.y += (end.y > current.y) ? 1 : -1;
            }

            tilemap.SetTile(new Vector3Int(current.x, current.y, 0), tileFloor);
        }
    }
}