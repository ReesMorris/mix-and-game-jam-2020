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
    if (fightManager.Fighting() && !animationRunning && fightManager.currentTurn == FightManager.Turn.Mob && !defeated) {
      Attack(CurrentDamage);
    }
  }

  // Finding Gameobject model for attacking animation
  public void SetFakeMob(string name) {
    fakeMob = GameObject.Find(name);
    fakeMob.gameObject.SetActive(false);
  }

  public void Attack(int damage) {
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
      animationRunning = true;
      gameObject.GetComponent<SpriteRenderer>().enabled = false;
      fakeMob.gameObject.SetActive(true);
      fakeMob.gameObject.transform.position = new Vector3(FightSpawn.position.x, FightSpawn.position.y, FightSpawn.position.z);
      fakeMob.GetComponent<Animator>().Play("FakeCrab_Attacking");
      yield return new WaitForSeconds(0.8f);
      GameObject.Find("Player").GetComponent<PlayerFight>().HealthDamaged(damage);
      yield return new WaitForSeconds(1.5f);
      fakeMob.gameObject.SetActive(false);
      gameObject.GetComponent<SpriteRenderer>().enabled = true;
      animationRunning = false;
      fightManager.currentTurn = FightManager.Turn.Player;
      fightManager.ButtonsActive(true);
    }
  }

  private IEnumerator EndFight() {
    defeated = true;
    fightManager.currentTurn = FightManager.Turn.Player;
    fightManager.ButtonsActive(false);
    gameObject.GetComponent<Animator>().Play("Crab_Dead");
    yield return new WaitForSeconds(2f);
    dead = true;
    fightManager.CanFight(false);
    ResetForNextFight();
    Destroy(gameObject);
  }

  private void ResetForNextFight() {
    fakeMob.gameObject.SetActive(true);
    fightManager.ShowFightUI(false);
    Player.GetComponent<PlayerFight>().ResetForNextFight();
  }


}
