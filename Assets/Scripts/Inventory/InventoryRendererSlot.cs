﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryRendererSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

  public Image image;
  public TMPro.TMP_Text count;
  public GameObject tooltipObject;
  public TMPro.TMP_Text tooltipName;
  public TMPro.TMP_Text tooltipDesc;

  private bool hasItem = false;

  public void UpdateDisplay(int _count, Item item) {
    image.sprite = item.sprite;
    count.text = _count.ToString();
    tooltipName.text = item.name;
    tooltipDesc.text = item.tooltipText;
    hasItem = _count > 0;
  }

  public void OnPointerEnter(PointerEventData eventData) {
    if (hasItem)
      tooltipObject.SetActive(true);
  }

  public void OnPointerExit(PointerEventData eventData) {
    if (hasItem)
      tooltipObject.SetActive(false);
  }
}