﻿using System.Collections;
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
    // Empty the array on change to prevent render bugs
    // PS: This is really inefficient :)
    foreach (InventoryRendererSlot slot in slots)
      Destroy(slot.gameObject);
    slots.Clear();

    for (int i = 0; i < inventory.Count; i++) {
      InventorySlot item = inventory[i];

      if (item != null) {
        if (item.item.type != Item.Types.Craftable) {
          slots.Add(Instantiate(slotPrefab, gameObject.transform));
          slots[slots.Count - 1].UpdateDisplay(item.count, item.item);
        }
      }
    }
  }
}
