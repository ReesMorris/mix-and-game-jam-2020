using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableTile : MonoBehaviour {

  public Image image;
  public TMPro.TMP_Text count;

  private TableTileItem tableItem;
  private InventoryManager inventoryManager;
  private bool playerClose;

  void Start() {
    tableItem = new TableTileItem(null, 0);
    inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    InventoryRendererSlot.onInventorySlotClick += OnInventorySlotClick;
  }

  void OnInventorySlotClick(Item item) {
    if (playerClose && item.saleValue > 0) {
      if (!tableItem.item) tableItem = new TableTileItem(item, 0);
      if (tableItem.item && item == tableItem.item) {
        tableItem.quantity++;
        inventoryManager.RemoveItemFromInventory(item, 1);
        UpdateUI();
      }
    }
  }

  void UpdateUI() {
    image.gameObject.SetActive(tableItem.item != null);
    count.gameObject.SetActive(tableItem.item != null);

    if (tableItem.item) {
      image.sprite = tableItem.item.sprite;
      count.text = "x" + tableItem.quantity;
    }
  }

  private void OnMouseOver() {
    if (Input.GetMouseButtonDown(1)) {
      if (tableItem != null && tableItem.quantity > 0) {
        tableItem.quantity--;
        inventoryManager.AddItemToInventory(tableItem.item, 1);
        if (tableItem.quantity == 0)
          tableItem = new TableTileItem(null, 0);
        UpdateUI();
      }
    }
  }

  public TableTileItem GetItem() {
    return tableItem;
  }

  public void OnPlayerEnter() {
    playerClose = true;
  }
  public void OnPlayerExit() {
    playerClose = false;
  }
}
