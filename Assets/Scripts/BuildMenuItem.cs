using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildMenuItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

  public Image image;
  public TMPro.TMP_Text itemName;
  public GameObject tooltipObject;
  public TMPro.TMP_Text tooltipText;

  private Image itemBackground;
  private CraftingRecipe recipe;
  private InventoryManager inventoryManager;
  private BuildableAreaManager buildableAreaManager;
  private bool hasAllIngredients;

  void Start() {
    InventoryManager.onInventoryChange += OnInventoryChange;
  }

  public void Init(CraftingRecipe _recipe) {
    inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    buildableAreaManager = GameObject.Find("GameManager").GetComponent<BuildableAreaManager>();
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

      if (recipe.ingredients.Length > 0) {
        foreach (CraftingRecipeItem ingredient in recipe.ingredients) {
          int inventoryCount = inventoryManager.ItemCount(ingredient.item);

          tooltipText.text += "<br>";
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
    buildableAreaManager.SetRecipe(recipe);
  }
}
