using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour {

  private bool fighting = false;
  private Mob opponent;
  private GameObject player;

  public Turn currentTurn;
  public GameObject[] FakeMobs;

  public enum Turn {
    Player,
    Mob,
  }
  public List<Button> Buttons;

  private void Start() {
    currentTurn = Turn.Player;
    player = GameObject.Find("Player");
  }

  public void SetOpponent(Mob mob) {
    opponent = mob;

    switch (opponent.name) {
      case "Crab":
        opponent.SetFakeMob("Fake" + opponent.name); ;
        foreach (GameObject m in FakeMobs) {
          m.SetActive(false);
        }
        break;
      case "Bee":
        opponent.SetFakeMob("Fake" + opponent.name); ;
        foreach (GameObject m in FakeMobs) {
          m.SetActive(false);
        }
        break;
      case "Bird":
        opponent.SetFakeMob("Fake" + opponent.name); ;
        foreach (GameObject m in FakeMobs) {
          m.SetActive(false);
        }
        break;
      default:
        opponent.SetFakeMob("");
        break;
    }

  }

  public Mob GetOpponent() {
    return opponent;
  }

  public void CanFight(bool mode) {
    fighting = mode;
  }

  public void ActivateFakeMobs() {
    foreach (GameObject m in FakeMobs) {
      m.SetActive(true);
    }
  }

  public bool Fighting() {
    return fighting;
  }

  public void Flee() {
    print("!!! Player is fleeing");
    CanFight(false);
    player.GetComponent<PlayerFight>().ResetForNextFight();
    ActivateFakeMobs();
    opponent.transform.position = opponent.OldMobPosition;
    opponent.SetFakeMob("");
  }

  public void ButtonsActive(bool a) {
    foreach (Button b in Buttons) {
      b.interactable = a;
    }
  }

  public void ShowFightUI(bool activation) {
    if (activation) {
      foreach (Button b in Buttons) {
        b.GetComponent<Image>().gameObject.SetActive(true);
      }
      player.transform.Find("Canvas/PlayerHealthBar").gameObject.SetActive(true);
      opponent.transform.Find("Canvas/MobHealthBar").gameObject.SetActive(true);
    } else {
      foreach (Button b in Buttons) {
        b.GetComponent<Image>().gameObject.SetActive(false);
      }
      player.transform.Find("Canvas/PlayerHealthBar").gameObject.SetActive(false);
      opponent.transform.Find("Canvas/MobHealthBar").gameObject.SetActive(false);
    }
  }

}
