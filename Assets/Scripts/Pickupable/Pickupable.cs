using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour {

  public Item item;
  public int quantity = 1;
  public TMPro.TMP_Text text;
  public float RespawnTime;

  private InventoryManager inventoryManager;
  private SpriteRenderer spriteRenderer;
  private Vector3 OldPosition;

  void Start() {
    inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = item.sprite;
    OldPosition = transform.position;
  }

  public void Init(Item _item, int _quantity) {
    item = _item;
    quantity = _quantity;
  }

  public void Pickup() {
    inventoryManager.AddItemToInventory(item, quantity);
    StartCoroutine(MoveAndRespawn(RespawnTime));
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

  private IEnumerator MoveAndRespawn(float time) {
    // teleporting object
    transform.position = new Vector3(1000, 1000, transform.position.x);
    yield return new WaitForSeconds(time);
    transform.position = OldPosition;
  }
}
