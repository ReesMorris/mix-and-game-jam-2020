using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryRendererSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

  public delegate void OnInventorySlotClick(Item item);
  public static OnInventorySlotClick onInventorySlotClick;

  public Image image;
  public TMPro.TMP_Text count;
  public GameObject tooltipObject;
  public TMPro.TMP_Text tooltipName;
  public TMPro.TMP_Text tooltipDesc;

  private bool hasItem = false;
  private Item item;
  private Sprite noImage;

  void Start() {
    noImage = image.sprite;
  }

  public void UpdateDisplay(int _count, Item _item) {
    if (!_item) {
      hasItem = false;
      item = null;
      image.sprite = noImage;
      if (count) count.text = "";
    } else {
      image.sprite = _item.sprite;
      if (count) count.text = _count.ToString();
      tooltipName.text = _item.name;
      tooltipDesc.text = _item.tooltipText;
      hasItem = _count > 0;
      if (hasItem) item = _item;

      int saleValue = _item.saleValue;
      if (saleValue > 0)
        tooltipDesc.text += "<br><color=grey>Sells for " + saleValue + " Coins</color>";
    }
  }

  public void OnPointerEnter(PointerEventData eventData) {
    if (hasItem)
      tooltipObject.SetActive(true);
  }

  public void OnPointerExit(PointerEventData eventData) {
    if (hasItem)
      tooltipObject.SetActive(false);
  }

  public void OnClick() {
    if (hasItem && item && onInventorySlotClick != null)
      onInventorySlotClick(item);
  }
}
