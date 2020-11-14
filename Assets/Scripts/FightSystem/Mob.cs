using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mob : MonoBehaviour {
  public Slider slider;
  public int MaxHealth;
  public int CurrentHealth;

  private void Start() {
    CurrentHealth = MaxHealth;
    slider.value = CurrentHealth;
  }

}
