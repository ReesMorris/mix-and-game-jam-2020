using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenuRenderer : MonoBehaviour {

  public BuildMenuItem buildMenuItem;

  private List<BuildMenuItem> recipes;
  private CraftingManager craftingManager;

  void Start() {
    craftingManager = GameObject.Find("GameManager").GetComponent<CraftingManager>();
    recipes = new List<BuildMenuItem>();

    // Loop through all recipes
    foreach (CraftingRecipe craftingRecipe in craftingManager.recipes) {
      BuildMenuItem item = Instantiate(buildMenuItem, gameObject.transform);
      item.Init(craftingRecipe);
      recipes.Add(item);
    }

  }

}
