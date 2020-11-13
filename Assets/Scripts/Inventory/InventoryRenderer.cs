using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryRenderer : MonoBehaviour {

  public InventoryRendererSlot slotPrefab;
  public Transform hotbarContainer;

  private List<InventoryRendererSlot> slots;

  void Start() {
    slots = new List<InventoryRendererSlot>();
    InventoryManager.onInventoryChange += OnInventoryChange;

    InstantiateHotbar();
  }

  void InstantiateHotbar() {
    for (int i = 0; i < 10; i++)
      slots.Add(Instantiate(slotPrefab, hotbarContainer));
  }

  void OnInventoryChange(List<InventorySlot> inventory) {
    for (int i = 0; i < inventory.Count; i++) {
      InventorySlot item = inventory[i];
      if (item != null)
        slots[i].UpdateDisplay(item.count, item.item);
    }
  }
}
