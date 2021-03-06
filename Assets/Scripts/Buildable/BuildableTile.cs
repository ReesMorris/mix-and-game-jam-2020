﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableTile : MonoBehaviour {

  public delegate void OnTilePlaced(Item tile);
  public static OnTilePlaced onTilePlaced;

  public Sprite whiteTile;
  public AudioClip demolishSound;
  public AudioClip placeSound;

  private bool empty = true;
  private SpriteRenderer spriteRenderer;
  private Item placedTile;
  private Color color;
  private Item selectedTile;
  private BuildableAreaManager buildableAreaManager;
  private AudioManager audioManager;

  private void Start() {
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    audioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
    color = spriteRenderer.material.color;

    color.a = 0.2f;
    spriteRenderer.material.color = color;
  }

  public void Show() {
    gameObject.SetActive(true);

    if (!buildableAreaManager) buildableAreaManager = GameObject.Find("GameManager").GetComponent<BuildableAreaManager>();
    selectedTile = buildableAreaManager.GetSelectedItem();
  }

  public bool IsEmpty() {
    return empty;
  }

  public Item GetSelectedTile() {
    return selectedTile;
  }

  public Item GetPlacedTile() {
    return placedTile;
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
            audioManager.PlaySound(demolishSound, true);
            empty = true;
            placedTile = null;
            gameObject.layer = 8;
            UpdateTileSprite();
          }
        }
      }

      // Build tool
      else {
        // Build item
        if (Input.GetMouseButtonDown(0) && empty) {
          if (buildableAreaManager.CanBuildSelectedItem()) {
            audioManager.PlaySound(placeSound, true);
            color.a = 1f;
            spriteRenderer.material.color = color;
            empty = false;
            gameObject.layer = 0;
            placedTile = selectedTile;
            UpdateTileSprite();
            if (onTilePlaced != null) onTilePlaced(selectedTile);
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
