using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // List to store all the rooms
    public List<GameObject> rooms = new List<GameObject>();

    // List to store all the creatures
    public List<GameObject> creatures = new List<GameObject>();

    public List<Adventurer> adventurers = new List<Adventurer>();

    // Prefabs for room, creature (Rat), and adventurer
    public GameObject roomPrefab;
    public GameObject ratPrefab;
    public GameObject adventurerPrefab;

    void Start()
    {
        Debug.Log("GameManager Initialized");

        SpawnAdventurer();

    }

    void Update()
    {
        // Check for left mouse click to place a room
        if (Input.GetMouseButtonDown(0))
        {
            PlaceRoom();
        }

        // Check for right mouse click to spawn a rat
        if (Input.GetMouseButtonDown(1))
        {
            SpawnRat();
        }

        // Press "A" to spawn an adventurer
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnAdventurer();
        }

        // Press "T" to trigger a turn
        if (Input.GetKeyDown(KeyCode.T))
        {
            ProcessTurn();
        }
    }

    void ProcessTurn()
    {
        foreach (Adventurer adventurer in adventurers)
        {
            // Make each adventurer take a turn
            adventurer.GetComponent<Adventurer>().TakeTurn();
        }
    }


    // Method to place a room
    void PlaceRoom()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        // Snap to the nearest grid cell (assuming 1 unit per grid cell)
        Vector3 snappedPos = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y), 0);

        GameObject newRoom = Instantiate(roomPrefab, snappedPos, Quaternion.identity);
        rooms.Add(newRoom);
    }

    // Method to spawn a rat
    void SpawnRat()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        // Snap to the nearest grid cell (assuming 1 unit per grid cell)
        Vector3 snappedPos = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y), 0);

        GameObject newRat = Instantiate(ratPrefab, snappedPos, Quaternion.identity);
        creatures.Add(newRat);
    }

    // Method to spawn an adventurer
    public void SpawnAdventurer()
    {
        Vector3 spawnPos = new Vector3(Random.Range(1, 59), Random.Range(1, 29), 0);
        GameObject adventurerObject = Instantiate(adventurerPrefab, spawnPos, Quaternion.identity);
        Adventurer adventurer = adventurerObject.GetComponent<Adventurer>();
        adventurers.Add(adventurer);
    }

}
