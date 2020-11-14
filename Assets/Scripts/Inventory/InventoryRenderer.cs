using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryRenderer : MonoBehaviour {

  public InventoryRendererSlot slotPrefab;

  private List<InventoryRendererSlot> slots;

  void Start() {
    slots = new List<InventoryRendererSlot>();
    InventoryManager.onInventoryChange += OnInventoryChange;
  }



  void OnInventoryChange(List<InventorySlot> inventory) {

    // Empty the array to prevent render bugs
    // This is really inefficient ..
    foreach (InventoryRendererSlot slot in slots)
      Destroy(slot.gameObject);
    slots.Clear();

    for (int i = 0; i < inventory.Count; i++) {
      InventorySlot item = inventory[i];

      if (item != null) {
        slots.Add(Instantiate(slotPrefab, gameObject.transform));
        slots[i].UpdateDisplay(item.count, item.item);
      }
    }
  }
}
