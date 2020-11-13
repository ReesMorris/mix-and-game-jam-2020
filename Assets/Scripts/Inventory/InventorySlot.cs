[System.Serializable]
public class InventorySlot {

  public InventorySlot(Item _item, int _count) {
    item = _item;
    count = _count;
  }

  public Item item;
  public int count;
}
