using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour {
  public int MaxHealth;
  public int CurrentHealth;

  private void Start() {
    CurrentHealth = MaxHealth;
  }

  public void GetDamage(int damage) {
    CurrentHealth -= damage;
  }

}
