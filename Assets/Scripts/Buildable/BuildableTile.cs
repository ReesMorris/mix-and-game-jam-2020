using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableTile : MonoBehaviour {

  private bool empty = true;
  private SpriteRenderer spriteRenderer;
  private Color color;

  public Sprite whiteTile;
  public Sprite selectedTile;

  private void Start() {
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    color = spriteRenderer.material.color;

    color.a = 0.2f;
    spriteRenderer.material.color = color;
  }

  public void EnableTile() {
    gameObject.SetActive(true);
  }

  public void DisableTile() {
    if (empty) {
      spriteRenderer.sprite = whiteTile;
      gameObject.SetActive(false);
    }
  }

  public void UpdateTileSprite() {
    if (empty) {
      color.a = 0.2f;
      spriteRenderer.material.color = color;
      spriteRenderer.sprite = whiteTile;
    } else {
      spriteRenderer.sprite = selectedTile;
    }
  }

  private void OnMouseOver() {
    if (Input.GetMouseButtonDown(0) && empty) {
      color.a = 1f;
      spriteRenderer.material.color = color;
      empty = false;
      UpdateTileSprite();
    }
    if (Input.GetMouseButtonDown(1) && !empty) {
      empty = true;
      UpdateTileSprite();
    }
    if (empty) {
      spriteRenderer.sprite = selectedTile;
    }
  }

  private void OnMouseExit() {
    if (empty) {
      spriteRenderer.sprite = whiteTile;
    }
  }
}
