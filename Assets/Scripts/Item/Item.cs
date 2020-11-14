using UnityEngine;


[CreateAssetMenu(menuName = "Game / Item", order = 999)]
public class Item : ScriptableObject {
  public enum Types { None, Buildable, Craftable }

  public int id;
  public string itemName;
  public Types type;
  public int maxStackSize = 64;
  public Sprite sprite;
  public string tooltipText;
  public bool maxOnlyOne;
}
