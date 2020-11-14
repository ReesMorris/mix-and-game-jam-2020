using UnityEngine;

public class NavbarItemInventoryListener : MonoBehaviour {

  public GameObject plusAnimationPrefab;

  void Start() {
    InventoryManager.onInventoryIncrease += OnInventoryIncrease;
  }

  void OnInventoryIncrease(int amount) {
    if (amount > 0) {
      PlusAnimation anim = Instantiate(plusAnimationPrefab, transform).GetComponent<PlusAnimation>();
      anim.SetText("+" + amount);
    }
  }

}
