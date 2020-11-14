using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour {

  public List<Pickupable> pickupablesInRange;
  public int targetPickupable = -1;

  void Start() {
    pickupablesInRange = new List<Pickupable>();
    GetComponent<MeshRenderer>().enabled = false;
  }

  void Update() {
    if (Input.GetKeyUp(KeyCode.Tab) && pickupablesInRange.Count > 1) {
      SetTargetPickupable((targetPickupable + 1) % pickupablesInRange.Count);
    }


    if (Input.GetKeyUp((KeyCode.F)) && pickupablesInRange.Count > 0) {
      if (targetPickupable != -1) {
        pickupablesInRange[targetPickupable].Pickup();
        pickupablesInRange.RemoveAt(targetPickupable);
        SetTargetPickupable(0);
      }
    }
  }

  private void OnTriggerEnter(Collider other) {
    Pickupable pickupable = other.transform.parent.GetComponent<Pickupable>();

    if (pickupable != null) {
      pickupablesInRange.Add(pickupable);
      SetTargetPickupable(pickupablesInRange.Count - 1);
    }
  }

  private void OnTriggerExit(Collider other) {
    Pickupable pickupable = other.transform.parent.GetComponent<Pickupable>();

    if (pickupable != null) {
      pickupablesInRange.Remove(pickupable);
      SetTargetPickupable(0);
    }
  }

  private void SetTargetPickupable(int index) {
    if (targetPickupable != -1 && pickupablesInRange.Count >= targetPickupable)
      pickupablesInRange[targetPickupable].HideText();

    if (pickupablesInRange.Count == 0)
      targetPickupable = -1;

    else {
      targetPickupable = index;
      pickupablesInRange[targetPickupable].ShowText(pickupablesInRange.Count > 1);
    }
  }

}
