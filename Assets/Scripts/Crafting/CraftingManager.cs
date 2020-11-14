using UnityEngine;

public class CraftingManager : MonoBehaviour {

  public CraftingRecipe tempRecipe; // TODO(REES): Remove

  private InventoryManager inventoryManager;

  void Start() {
    inventoryManager = GetComponent<InventoryManager>();
  }

  void Update() {
    if (Input.GetKeyUp(KeyCode.Alpha0)) CraftRecipe(tempRecipe);
  }

  // Craft a recipe, if the player has enough inventory space
  public void CraftRecipe(CraftingRecipe recipe) {
    bool canCraft = true;
    foreach (CraftingRecipeItem ingredient in recipe.ingredients) {
      if (inventoryManager.ItemCount(ingredient.item) < ingredient.quantity) canCraft = false;
    }

    if (canCraft) {
      foreach (CraftingRecipeItem ingredient in recipe.ingredients)
        inventoryManager.RemoveItemFromInventory(ingredient.item, ingredient.quantity);

      inventoryManager.AddItemToInventory(recipe.output.item, recipe.output.quantity);
    }
  }

}
