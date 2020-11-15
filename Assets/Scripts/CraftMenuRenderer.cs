using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftMenuRenderer : MonoBehaviour {

  public CraftMenuItem craftMenuItem;

  private CraftingManager craftingManager;
  private List<CraftMenuItem> recipes;

  void Start() {
    craftingManager = GameObject.Find("GameManager").GetComponent<CraftingManager>();
    recipes = new List<CraftMenuItem>();

    // Loop through all recipes
    foreach (CraftingRecipe craftingRecipe in craftingManager.craftables) {
      CraftMenuItem item = Instantiate(craftMenuItem, gameObject.transform);
      item.Init(craftingRecipe);
      recipes.Add(item);
    }
  }


}
