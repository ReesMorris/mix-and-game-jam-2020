using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableTile : MonoBehaviour {

  public delegate void OnItemAddedToTable(Item item);
  public static OnItemAddedToTable onItemAddedToTable;

  public Image image;
  public TMPro.TMP_Text count;
  public Item tablePrefab;

  private TableTileItem tableItem;
  private BuildableTile buildableTile;
  private InventoryManager inventoryManager;
  public bool playerClose;

  void Start() {
    tableItem = new TableTileItem(null, 0);
    buildableTile = GetComponent<BuildableTile>();
    inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    InventoryRendererSlot.onInventorySlotClick += OnInventorySlotClick;
  }

  void OnInventorySlotClick(Item item) {
    if (buildableTile.GetSelectedTile() == tablePrefab) {
      if (playerClose && item.saleValue > 0) {
        if (!tableItem.item) tableItem = new TableTileItem(item, 0);
        if (tableItem.item && item == tableItem.item) {
          tableItem.quantity++;
          inventoryManager.RemoveItemFromInventory(item, 1);
          if (onItemAddedToTable != null) onItemAddedToTable(item);
          UpdateUI();
        }
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

  public void RemoveItem() {
    if (tableItem != null && tableItem.quantity > 0) {
      tableItem.quantity--;
      inventoryManager.AddItemToInventory(tableItem.item, 1);
      if (tableItem.quantity == 0)
        tableItem = new TableTileItem(null, 0);
      UpdateUI();
    }
  }

  private void OnMouseOver() {
    if (Input.GetMouseButtonDown(1)) {
      RemoveItem();
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
