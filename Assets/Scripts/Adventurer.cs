using System.Collections.Generic;
using UnityEngine;

public class Adventurer : MonoBehaviour
{
    private Vector3 targetPosition;

    private Inventory inventory;

    void Start()
    {
        // Set initial position to align with the grid
        targetPosition = transform.position;
        inventory = new Inventory(10);
        // Create a new item
        Item sword = new Item("Sword", "A sharp blade", 10);

        // Add the item to the adventurer's inventory
        AddItemToInventory(sword);
    }

    public void TakeTurn()
    {
        // Move in a random cardinal or intercardinal direction (1 unit)
        Vector3[] directions = {
            Vector3.up, Vector3.down, Vector3.left, Vector3.right,
            new Vector3(1, 1, 0), new Vector3(1, -1, 0), new Vector3(-1, 1, 0), new Vector3(-1, -1, 0)
        };

        // Choose a random direction and update the target position
        Vector3 direction = directions[Random.Range(0, directions.Length)];
        targetPosition += direction;

        // Snap to the grid (optional)
        targetPosition = new Vector3(Mathf.Round(targetPosition.x), Mathf.Round(targetPosition.y), 0);

        // Move adventurer to the target position
        transform.position = targetPosition;
    }

    public void AddItemToInventory(Item item)
    {
        bool added = inventory.AddItem(item);
        if (added)
        {
            Debug.Log("Item added to inventory: " + item.Name);
        }
        else
        {
            Debug.Log("Item full, could not add: " + item.Name);
        }
    }

    public void DropLoot(GridSquare gridSquare)
    {
        List<Item> itemsToDrop = inventory.GetItems();
        foreach (Item item in itemsToDrop)
        {
            gridSquare.AddItemToGrid(item);
        }
        inventory.ClearInventory(); // Clear the adventurer's inventory after dropping items
    }


}