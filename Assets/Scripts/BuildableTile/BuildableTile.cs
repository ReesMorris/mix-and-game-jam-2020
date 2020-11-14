using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildableTile : MonoBehaviour {

  public bool empty = true;

  public Sprite[] sprites;

  private SpriteRenderer spriteRenderer;
  private Color color;

  private void Start() {
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    color = spriteRenderer.material.color;

    color.a = 0.2f;
    spriteRenderer.material.color = color;
  }

  public void UpdateTile() {
    if (empty) {
      color.a = 0.2f;
      spriteRenderer.material.color = color;
      spriteRenderer.sprite = sprites[0];
    } else {
      spriteRenderer.sprite = sprites[1];
    }
  }

  void OnMouseOver() {
    if (Input.GetMouseButtonDown(0) && empty) {
      color.a = 1f;
      spriteRenderer.material.color = color;
      empty = false;
      UpdateTile();
    }
    if (Input.GetMouseButtonDown(1) && !empty) {
      empty = true;
      UpdateTile();
    }
    if (empty) {
      spriteRenderer.sprite = sprites[1];
    }
  }

  void OnMouseExit() {
    if (empty) {
      spriteRenderer.sprite = sprites[0];
    }
  }
}
