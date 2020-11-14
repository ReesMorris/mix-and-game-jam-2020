using UnityEngine;

[CreateAssetMenu(menuName = "Game / CraftingRecipe", order = 999)]
public class CraftingRecipe : ScriptableObject {
  public CraftingRecipeItem[] ingredients;
  public CraftingRecipeItem output;
}
