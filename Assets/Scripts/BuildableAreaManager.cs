using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableAreaManager : MonoBehaviour {

  public delegate void OnGridToggle(bool visible);
  public static OnGridToggle onGridToggle;

  private CraftingRecipe selectedRecipe;
  private InventoryManager inventoryManager;
  private bool showGrid;

  void Start() {
    inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
  }

  public void SetRecipe(CraftingRecipe item) {
    print(item);
    selectedRecipe = item;
    SetGridVisible(item != null);
  }

  public Item GetSelectedItem() {
    if (!selectedRecipe) return null;
    return selectedRecipe.output.item;
  }

  public void SetGridVisible(bool visible) {
    showGrid = visible;
    if (onGridToggle != null)
      onGridToggle(visible);
  }

  public bool CanBuildSelectedItem() {
    if (!selectedRecipe) return false;

    bool canBuild = true;
    foreach (CraftingRecipeItem item in selectedRecipe.ingredients) {
      if (inventoryManager.ItemCount(item.item) < item.quantity) canBuild = false;
    }
    return canBuild;
  }

  public void OnTilePlaced() {
    if (selectedRecipe && selectedRecipe.ingredients.Length > 0) {
      foreach (CraftingRecipeItem item in selectedRecipe.ingredients) {
        inventoryManager.RemoveItemFromInventory(item.item, item.quantity);
      }
    }
  }
}
