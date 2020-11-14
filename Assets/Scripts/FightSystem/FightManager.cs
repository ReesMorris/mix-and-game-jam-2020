using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour {
  private bool fighting = false;

  public void CanFight(bool mode) {
    fighting = mode;
  }

  public bool Fighting() {
    return fighting;
  }

}
