using UnityEngine;

public class Pickupable : MonoBehaviour {

  public Item item;
  public TMPro.TMP_Text text;

  private InventoryManager inventoryManager;
  private SpriteRenderer spriteRenderer;

  void Start() {
    inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = item.sprite;
  }

  public void Pickup() {
    inventoryManager.AddItemToInventory(item, 1);
    Destroy(gameObject);
  }

  public void ShowText(bool withTabText) {
    text.text = "[F] " + item.itemName;
    if (withTabText) text.text += "<br><size=0.8>[TAB] CYCLE</size>";
    text.gameObject.SetActive(true);
  }
  public void HideText() {
    text.gameObject.SetActive(false);
  }
}
