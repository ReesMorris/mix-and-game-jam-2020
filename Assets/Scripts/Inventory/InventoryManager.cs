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
    if (Input.GetKeyUp(KeyCode.Alpha1)) AddItemToInventory(apple);
    if (Input.GetKeyUp(KeyCode.Alpha2)) AddItemToInventory(banana);
  }

  // Adds an item to the inventory
  public bool AddItemToInventory(Item item) {
    // See if it exists in a slot that's not full
    foreach (InventorySlot i in inventoryItems) {
      if (i.item.id == item.id && i.count < item.maxStackSize) {
        i.count += 1;
        onInventoryChange(inventoryItems);
        return true;
      }
    }

    // Add it to a new slot, if not full
    if (inventoryItems.Count < slots) {
      inventoryItems.Add(new InventorySlot(item, 1));
      onInventoryChange(inventoryItems);
      return true;
    }

    // There are no slots!
    return false;
  }
}
