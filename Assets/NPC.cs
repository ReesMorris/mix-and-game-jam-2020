using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour {

  private List<Transform> beforeShopWaypoints;
  private List<Transform> afterShopWaypoints;
  private SpriteRenderer spriteRenderer;
  private int currentWaypoint;
  private Transform currentWaypointTransform;
  private float speed;
  private bool willShop;
  private TableTile targetTable;
  private bool isUsingBeforeShopWaypoints;

  public void Init(List<Transform> _beforeShopWaypoints, List<Transform> _afterShopWaypoints) {
    beforeShopWaypoints = _beforeShopWaypoints;
    afterShopWaypoints = _afterShopWaypoints;

    // willShop = Random.Range(0, 100) > 35;
    willShop = true;

    speed = Random.Range(1f, 2f);
    currentWaypoint = 1;
    currentWaypointTransform = beforeShopWaypoints[currentWaypoint];
    isUsingBeforeShopWaypoints = true;

    spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.color = new Color(1, 1, 1, 0);
    transform.position = beforeShopWaypoints[0].position;
    StartCoroutine(FadeIn());
  }

  IEnumerator FadeIn() {
    while (spriteRenderer.color.a < 0.8) {
      spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a + 0.05f);
      yield return new WaitForSeconds(0.1f);
    }
  }

  void Update() {
    // Reached destination?
    if (transform.position == currentWaypointTransform.position) {
      // beforeShop
      if (isUsingBeforeShopWaypoints) {
        if (beforeShopWaypoints.Count > currentWaypoint + 1) {
          currentWaypoint += 1;
          currentWaypointTransform = beforeShopWaypoints[currentWaypoint];
        }
      }

      // shopDest
      else if (willShop && !targetTable) {

      }

    }

    // Not reached destination
    else {
      if (currentWaypointTransform != null)
        transform.position = Vector3.MoveTowards(transform.position, currentWaypointTransform.position, speed * Time.deltaTime);
    }
  }
}
