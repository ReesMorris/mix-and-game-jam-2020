using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableTileTrigger : MonoBehaviour {

  private MeshRenderer meshRenderer;
  private TableTile tableTile;
  private BuildableTile parent;

  void Start() {
    GetComponent<MeshRenderer>().enabled = false;
    parent = transform.parent.GetComponent<BuildableTile>();
    tableTile = transform.parent.GetComponent<TableTile>();
  }

  private void OnTriggerEnter(Collider other) {
    if (!parent.IsEmpty()) {
      if (other.gameObject.tag.Equals("Player")) {
        tableTile.OnPlayerEnter();
      }
    }
  }

  private void OnTriggerExit(Collider other) {
    if (!parent.IsEmpty()) {
      if (other.gameObject.tag.Equals("Player")) {
        tableTile.OnPlayerExit();
      }
    }
  }
}
