// File: Inventory.cs

using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> items;
    private int capacity;

    // Constructor to set a capacity (optional)
    public Inventory(int capacity = -1)
    {
        this.capacity = capacity;
        items = new List<Item>();
    }

    // Method to add an item to the inventory
    public bool AddItem(Item item)
    {
        if (capacity == -1 || items.Count < capacity)
        {
            items.Add(item);
            return true;
        }
        return false; // Inventory is full
    }

    // Method to remove an item from the inventory
    public bool RemoveItem(Item item)
    {
        return items.Remove(item);
    }

    // Method to get the list of items
    public List<Item> GetItems()
    {
        return new List<Item>(items); // Return a copy to prevent modification
    }

    // Method to check if the inventory contains an item
    public bool ContainsItem(Item item)
    {
        return items.Contains(item);
    }

    // Method to clear the inventory
    public void ClearInventory()
    {
        items.Clear();
    }
}

// The Item class represents an item in the inventory.
public class Item
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Value { get; private set; }

    // Constructor
    public Item(string name, string description, int value)
    {
        Name = name;
        Description = description;
        Value = value;
    }
}