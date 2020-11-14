using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupableTrigger : MonoBehaviour {

  private List<Pickupable> pickupablesInRange;
  private int targetPickupable = -1;

  void Start() {
    pickupablesInRange = new List<Pickupable>();
    GetComponent<MeshRenderer>().enabled = false;
  }

  // Listen for keyup events
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

  // Called when the player enters a pickupable collider
  private void OnTriggerEnter(Collider other) {
    Pickupable pickupable = other.transform.parent.GetComponent<Pickupable>();

    if (pickupable != null) {
      pickupablesInRange.Add(pickupable);
      SetTargetPickupable(pickupablesInRange.Count - 1);
    }
  }

  // Called when the player exits a pickupable collider
  private void OnTriggerExit(Collider other) {
    Pickupable pickupable = other.transform.parent.GetComponent<Pickupable>();

    if (pickupable != null) {
      pickupablesInRange.Remove(pickupable);
      SetTargetPickupable(0);
    }
  }

  // Set the target index for the item in range
  private void SetTargetPickupable(int index) {
    if (targetPickupable != -1 && pickupablesInRange.Count > targetPickupable)
      pickupablesInRange[targetPickupable].HideText();

    if (pickupablesInRange.Count == 0) {
      targetPickupable = -1;
    } else {
      targetPickupable = index;
      pickupablesInRange[targetPickupable].ShowText(pickupablesInRange.Count > 1);
    }
  }

}
