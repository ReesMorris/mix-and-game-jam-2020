using UnityEngine;

public class Pickupable : MonoBehaviour {

  public Item item;
  public int quantity = 1;
  public TMPro.TMP_Text text;

  private InventoryManager inventoryManager;
  private SpriteRenderer spriteRenderer;

  void Start() {
    inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = item.sprite;
  }

  public void Pickup() {
    inventoryManager.AddItemToInventory(item, quantity);
    Destroy(gameObject);
  }

  public void ShowText(bool withTabText) {
    if (quantity == 1) text.text = "[F] " + item.itemName;
    if (quantity > 1) text.text = "[F] " + item.itemName + " (x" + quantity + ")";
    if (withTabText) text.text += "<br><size=0.8>[TAB] CYCLE</size>";
    text.gameObject.SetActive(true);
  }
  public void HideText() {
    text.gameObject.SetActive(false);
  }
}
