using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

  public delegate void OnInventoryChange(List<InventorySlot> inventory);
  public static OnInventoryChange onInventoryChange;

  public Item apple; // TODO(REES): Remove
  public Item banana; // TODO(REES): Remove
  public int slots = 10;

  private List<InventorySlot> inventoryItems;

  void Start() {
    inventoryItems = new List<InventorySlot>();
  }

  void Update() {
    if (Input.GetKeyUp(KeyCode.Alpha1)) AddItemToInventory(apple, 100);
    if (Input.GetKeyUp(KeyCode.Alpha2)) AddItemToInventory(banana, 1);
  }

  // Adds an item to the inventory
  public void AddItemToInventory(Item item, int count) {
    int itemsAdded = 0;

    // Loop through all inventory items and update existing counts
    foreach (InventorySlot slot in inventoryItems) {
      if (slot != null) {
        if (slot.item.id == item.id) {
          while (itemsAdded < count && !SlotIsFull(slot)) {
            slot.count += 1;
            itemsAdded++;
          }
        }
      }
    }

    // Add new counts where necessary
    if (itemsAdded < count) {
      while (itemsAdded < count && !InventoryIsFull()) {
        int amountToAdd = Mathf.Min(count - itemsAdded, item.maxStackSize);
        itemsAdded += amountToAdd;

        InventorySlot newSlot = new InventorySlot(item, amountToAdd);
        int firstFreeSlot = FirstFreeSlot();
        if (firstFreeSlot == -1) inventoryItems.Add(newSlot);
        else inventoryItems[firstFreeSlot] = newSlot;
      }
    }

    // Call the delegate
    if (itemsAdded > 0)
      onInventoryChange(inventoryItems);
  }

  // Returns true if an inventory slot is full
  bool SlotIsFull(InventorySlot slot) {
    return slot.count >= slot.item.maxStackSize;
  }

  // Returns true if all inventory slots are used
  bool InventoryIsFull() {
    return inventoryItems.Count >= slots;
  }

  // Returns the first free inventory slot index, or -1 if all are taken
  int FirstFreeSlot() {
    for (int i = 0; i < inventoryItems.Count; i++)
      if (inventoryItems[i] == null) return i;
    return -1;
  }

  // Returns a boolean if a player has enough items in their inventory
  public int ItemCount(Item item) {
    int cumulativeCount = 0;
    foreach (InventorySlot slot in inventoryItems)
      if (slot != null)
        if (slot.item.id == item.id)
          cumulativeCount += slot.count;
    return cumulativeCount;
  }

  // Removes an item from inventory, ignoring whether the player has enough
  public void RemoveItemFromInventory(Item item, int count) {
    int itemsRemoved = 0;

    // Loop through all items in the inventory
    for (int i = 0; i < inventoryItems.Count; i++) {
      InventorySlot slot = inventoryItems[i];

      if (slot != null) {
        if (slot.item.id == item.id && itemsRemoved < count) {
          int toRemove = Mathf.Min(slot.count, count - itemsRemoved);
          slot.count -= toRemove;
          itemsRemoved += toRemove;

          if (slot.count <= 0)
            inventoryItems[i] = null;
        }
      }
    }

    if (itemsRemoved > 0)
      onInventoryChange(inventoryItems);
  }
}