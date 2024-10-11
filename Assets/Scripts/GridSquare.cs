using System.Collections.Generic;
using UnityEngine;

// Save this script as "GridSquare.cs" in your Unity project under the "Scripts" folder.
public class GridSquare : MonoBehaviour
{
    private Inventory inventory;

    void Start()
    {
        inventory = new Inventory(); // No capacity limit for grid squares
    }

    // Method to add an item to the grid square's inventory
    public void AddItemToGrid(Item item)
    {
        inventory.AddItem(item);
        Debug.Log("Item added to grid square: " + item.Name);
    }

    // Method to remove an item from the grid square's inventory
    public void RemoveItemFromGrid(Item item)
    {
        if (inventory.RemoveItem(item))
        {
            Debug.Log("Item removed from grid square: " + item.Name);
        }
        else
        {
            Debug.Log("Item not found in grid square: " + item.Name);
        }
    }

    // Method to get all items in the grid square's inventory
    public List<Item> GetItemsInGrid()
    {
        return inventory.GetItems();
    }
}
