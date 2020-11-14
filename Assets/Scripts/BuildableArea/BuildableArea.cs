using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableArea : MonoBehaviour {

  private List<BuildableTile> BuildableTiles;

  public BuildableTile buildableTile;
  public int HorizontalTiles;
  public int VerticalTiles;

  private void Start() {
    BuildableTiles = new List<BuildableTile>();
    for (int i = 0; i < HorizontalTiles; i++) {
      for (int j = 0; j < VerticalTiles; j++) {
        BuildableTiles.Add(Instantiate(buildableTile, new Vector3((i + transform.position.x), (j + transform.position.y), 0), Quaternion.identity));
      }
    }
    foreach (BuildableTile tile in BuildableTiles) {
      tile.transform.SetParent(gameObject.transform);
    }
  }
}
