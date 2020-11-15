using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour {

  public Item coinPrefab;

  private List<Transform> beforeShopWaypoints;
  private List<Transform> afterShopWaypoints;
  private SpriteRenderer spriteRenderer;
  private int currentWaypoint;
  private Transform currentWaypointTransform;
  private float speed;
  private bool willShop;
  private TableTile targetTable;
  private bool isUsingBeforeShopWaypoints;
  private bool boughtItem;
  private InventoryManager inventoryManager;

  public void Init(List<Transform> _beforeShopWaypoints, List<Transform> _afterShopWaypoints, Sprite skin) {
    inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    beforeShopWaypoints = _beforeShopWaypoints;
    afterShopWaypoints = _afterShopWaypoints;

    willShop = Random.Range(0, 100) > 35;

    speed = Random.Range(1f, 2f);
    currentWaypoint = 1;
    currentWaypointTransform = beforeShopWaypoints[currentWaypoint];
    isUsingBeforeShopWaypoints = true;

    spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.color = new Color(1, 1, 1, 0);
    transform.position = beforeShopWaypoints[0].position;
    spriteRenderer.sprite = skin;
    StartCoroutine(FadeIn());

    // Find a shop
    GameObject[] tiles = GameObject.FindGameObjectsWithTag("BuildableTile");
    List<TableTile> availableTiles = new List<TableTile>();
    foreach (GameObject tile in tiles) {
      if (!tile.GetComponent<BuildableTile>().IsEmpty()) {
        TableTile tableTile = tile.GetComponent<TableTile>();
        if (tableTile.GetItem() != null)
          availableTiles.Add(tableTile);
      }
    }
    if (availableTiles.Count == 0) willShop = false;
    else targetTable = availableTiles[Random.Range(0, availableTiles.Count)];

  }

  IEnumerator FadeIn() {
    while (spriteRenderer.color.a < 0.8) {
      spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a + 0.05f);
      yield return new WaitForSeconds(0.1f);
    }
  }

  void BuyItem() {
    TableTileItem item = targetTable.GetItem();
    if (item != null && item.item != null && item.quantity > 0) {
      targetTable.RemoveItem();
      inventoryManager.AddItemToInventory(coinPrefab, item.item.saleValue);
    }
    boughtItem = true;
  }

  void Update() {
    // Reached shop
    if (willShop && targetTable && transform.position == targetTable.transform.position) {
      if (boughtItem) willShop = false;
      else BuyItem();
    }

    // Reached destination?
    else if (transform.position == currentWaypointTransform.position) {
      // beforeShop
      if (isUsingBeforeShopWaypoints) {
        if (beforeShopWaypoints.Count > currentWaypoint + 1) {
          currentWaypoint += 1;
          currentWaypointTransform = beforeShopWaypoints[currentWaypoint];
        } else {
          isUsingBeforeShopWaypoints = false;
          currentWaypoint = 0;
        }
      }

      // shopDest Missing
      else if (willShop && (!targetTable || targetTable.GetItem().quantity == 0))
        willShop = false;

      // shopDest Found
      else if (willShop && transform.position != targetTable.gameObject.transform.position)
        currentWaypointTransform = targetTable.gameObject.transform;

      // noShopDest
      else {
        if (afterShopWaypoints.Count > currentWaypoint + 1) {
          currentWaypoint += 1;
          currentWaypointTransform = afterShopWaypoints[currentWaypoint];
        } else {
          Destroy(gameObject);
        }
      }
    }

    // Not reached destination
    else {
      if (currentWaypointTransform != null)
        transform.position = Vector3.MoveTowards(transform.position, currentWaypointTransform.position, speed * Time.deltaTime);
    }
  }
}
