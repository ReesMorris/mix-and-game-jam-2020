using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mob : MonoBehaviour {
  public Slider Slider;
  public int MaxHealth;
  public int CurrentHealth;
  public int CurrentDamage; // Damage that ca be inflicted to the player
  public bool dead = false;
  public Transform FightSpawn;
  public GameObject Player;
  [HideInInspector]
  public Vector3 OldMobPosition;

  private GameObject fakeMob;
  private FightManager fightManager;
  private bool animationRunning = false;
  private bool defeated = false;

  private void Start() {
    CurrentHealth = MaxHealth;
    Slider.maxValue = MaxHealth;
    Slider.value = CurrentHealth;

    fightManager = GameObject.Find("GameManager").GetComponent<FightManager>();
  }

  private void Update() {
    // print("Fighting: " + fightManager.Fighting());
    // print("Animation Running: " + animationRunning);
    // print("Turn:" + fightManager.currentTurn);
    // print("defeated: " + defeated);
    if (fightManager.Fighting() && !animationRunning && fightManager.currentTurn == FightManager.Turn.Mob && !defeated) {
      Attack(CurrentDamage);
    }
  }

  public void SetOldPosition(Vector3 pos) {
    OldMobPosition = pos;
  }

  // Finding Gameobject model for attacking animation
  public void SetFakeMob(string name) {
    fakeMob = GameObject.Find(name);
  }

  public void Attack(int damage) {
    StopCoroutine(AttackPhase(damage));
    StartCoroutine(AttackPhase(damage));
  }

  public void CheckHealth() {
    if (CurrentHealth <= 0) {
      StartCoroutine(EndFight());
    }
  }

  public void HealthRestored(int health) {
    if (CurrentHealth < MaxHealth) {
      CurrentHealth += health;
      Slider.value = CurrentHealth;
    }
    if (CurrentHealth > 100) {
      CurrentHealth = 100;
    }
  }

  public void HealthDamaged(int damage) {
    if (CurrentHealth > 0) {
      CurrentHealth -= damage;
      Slider.value = CurrentHealth;
      CheckHealth();
    }
  }

  private IEnumerator AttackPhase(int damage) {
    if (fakeMob != null) {
      // Disableing Mob main sprite
      gameObject.GetComponent<SpriteRenderer>().enabled = false;

      // Activating animated Mob for the attack animation
      fakeMob.gameObject.SetActive(true);
      fakeMob.gameObject.transform.position = new Vector3(FightSpawn.position.x, FightSpawn.position.y, FightSpawn.position.z);

      // Starting attack animation for mob
      animationRunning = true;
      fakeMob.GetComponent<Animator>().Play(fakeMob.name + "_Attacking");

      yield return new WaitForSeconds(0.8f);
      // Damaging Player
      GameObject.Find("Player").GetComponent<PlayerFight>().HealthDamaged(damage);

      yield return new WaitForSeconds(1.5f);

      // Deactivating animated mob
      fakeMob.gameObject.SetActive(false);

      // Enableing main sprite
      gameObject.GetComponent<SpriteRenderer>().enabled = true;

      // Stopping animation
      animationRunning = false;

      //Updating turn and showing buttons for player's turn
      fightManager.currentTurn = FightManager.Turn.Player;
      fightManager.ButtonsActive(true);

    }
  }

  private IEnumerator EndFight() {

    print("!!! Player defeated mob");

    // Interrupting animation if running
    animationRunning = false;

    // Setting mob to defeated
    defeated = true;

    // Setting next turn to player's turn and deactivating buttons
    fightManager.currentTurn = FightManager.Turn.Player;
    fightManager.ButtonsActive(false);

    // Playing mob's death animation
    gameObject.GetComponent<Animator>().Play(gameObject.name + "_Dead");

    yield return new WaitForSeconds(2f);

    // Mob now dead
    dead = true;

    // Fight is ended
    fightManager.CanFight(false);
    ResetForNextFight(); // Resetting
    fightManager.ActivateFakeMobs(); // Activating animated models for attacks
    Destroy(gameObject); // Mob is destroyed
  }

  private void ResetForNextFight() {
    // Deactivating Fight UI
    fightManager.ShowFightUI(false);

    // Resetting Player
    Player.GetComponent<PlayerFight>().ResetForNextFight();
  }


}
