using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftMenuItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

  public delegate void OnCraftMenuItemPurchase(Item item);
  public static OnCraftMenuItemPurchase onCraftMenuItemPurchase;

  public Image image;
  public TMPro.TMP_Text itemName;
  public GameObject tooltipObject;
  public TMPro.TMP_Text tooltipText;

  private Image itemBackground;
  private CraftingRecipe recipe;
  private InventoryManager inventoryManager;
  private bool hasAllIngredients;

  public void Init(CraftingRecipe _recipe) {
    InventoryManager.onInventoryChange += OnInventoryChange;
    inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    itemBackground = GetComponent<Image>();
    recipe = _recipe;

    itemName.text = recipe.output.item.name;
    image.sprite = recipe.output.item.sprite;
    UpdateTooltip();
  }

  // Update the tooltip text
  void UpdateTooltip() {
    if (recipe != null) {
      tooltipText.text = recipe.output.item.tooltipText;
      hasAllIngredients = true;

      if (inventoryManager.ItemCount(recipe.output.item) > 0) {
        hasAllIngredients = false;
        tooltipText.text += "<br><color=green>Already Owned</color>";
      }

      if (hasAllIngredients && recipe.ingredients.Length > 0) {
        tooltipText.text += "<br>";
        foreach (CraftingRecipeItem ingredient in recipe.ingredients) {
          int inventoryCount = inventoryManager.ItemCount(ingredient.item);

          if (inventoryCount >= ingredient.quantity) {
            tooltipText.text += "<color=green>";
          } else {
            tooltipText.text += "<color=orange>";
            hasAllIngredients = false;
          }
          tooltipText.text += inventoryCount + "/" + ingredient.quantity + " " + ingredient.item.name;
          tooltipText.text += "</color>";
        }
      }

      if (hasAllIngredients) itemBackground.color = new Color(0.4f, 0.85f, 1f);
      else itemBackground.color = new Color(0.85f, 0.43f, 1f);
    }
  }

  // Update tooltip text on inventory change
  void OnInventoryChange(List<InventorySlot> slots) {
    UpdateTooltip();
  }

  public void OnPointerEnter(PointerEventData eventData) {
    tooltipObject.SetActive(true);
  }

  public void OnPointerExit(PointerEventData eventData) {
    tooltipObject.SetActive(false);
  }

  public void OnClick() {
    if (hasAllIngredients) {
      inventoryManager.AddItemToInventory(recipe.output.item, recipe.output.quantity);
      foreach (CraftingRecipeItem item in recipe.ingredients)
        inventoryManager.RemoveItemFromInventory(item.item, item.quantity);

      if (onCraftMenuItemPurchase != null)
        onCraftMenuItemPurchase(recipe.output.item);
    }
  }
}
