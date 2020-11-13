using UnityEngine;

[CreateAssetMenu(menuName = "Game / Item", order = 999)]
public class Item : ScriptableObject {
  public int id;
  public string itemName;
  public int maxStackSize = 64;
  public Sprite sprite;
  public string tooltipText;
}
