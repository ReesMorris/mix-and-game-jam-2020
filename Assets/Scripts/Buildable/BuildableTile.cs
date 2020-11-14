﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableTile : MonoBehaviour {

  public delegate void OnTilePlaced();
  public static OnTilePlaced onTilePlaced;

  public Sprite whiteTile;

  private bool empty = true;
  private SpriteRenderer spriteRenderer;
  private Color color;
  private Item selectedTile;
  private BuildableAreaManager buildableAreaManager;

  private void Start() {
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    color = spriteRenderer.material.color;

    color.a = 0.2f;
    spriteRenderer.material.color = color;
  }

  public void Show() {
    gameObject.SetActive(true);

    if (!buildableAreaManager) buildableAreaManager = GameObject.Find("GameManager").GetComponent<BuildableAreaManager>();
    selectedTile = buildableAreaManager.GetSelectedItem();
  }

  public void Hide() {
    if (empty)
      gameObject.SetActive(false);
  }

  public void UpdateTileSprite() {
    if (empty) {
      color.a = 0.2f;
      spriteRenderer.material.color = color;
      spriteRenderer.sprite = whiteTile;
    } else {
      spriteRenderer.sprite = selectedTile.sprite;
    }
  }

  private void OnMouseOver() {
    if (selectedTile) {
      // Demolish tool
      if (selectedTile.itemName == "Demolish") {
        if (!empty) {
          color.a = 0.3f;
          spriteRenderer.material.color = color;

          if (Input.GetMouseButtonDown(0)) {
            empty = true;
            UpdateTileSprite();
          }
        }
      }

      // Build tool
      else {
        // Build item
        if (Input.GetMouseButtonDown(0) && empty) {
          if (buildableAreaManager.CanBuildSelectedItem()) {
            color.a = 1f;
            spriteRenderer.material.color = color;
            empty = false;
            UpdateTileSprite();
            buildableAreaManager.OnTilePlaced();
          }
        }

        // Show ghost preview
        if (empty) {
          spriteRenderer.sprite = selectedTile.sprite;
        }
      }
    }
  }

  private void OnMouseExit() {
    if (selectedTile) {
      if (empty) {
        spriteRenderer.sprite = whiteTile;
      } else {
        color.a = 1f;
        spriteRenderer.material.color = color;
      }
    }
  }
}
