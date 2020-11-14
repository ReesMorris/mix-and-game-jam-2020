using UnityEngine;

public class PickupableTrigger : MonoBehaviour {

  public Pickupable parent;

  void Start() {
    GetComponent<MeshRenderer>().enabled = false;
  }

  private void OnTriggerEnter(Collider other) {
    parent.text.gameObject.SetActive(true);
  }

  private void OnTriggerExit(Collider other) {
    parent.text.gameObject.SetActive(false);
  }


}
