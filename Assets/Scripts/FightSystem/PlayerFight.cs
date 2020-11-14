using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFight : MonoBehaviour {
  public Slider slider;
  public int MaxHealth;
  public int CurrentHealth;
  public int CurrentDamage;
  public GameObject fakePlayer;

  private FightManager fightManager;
  private Mob opponent;
  private Animator animator;
  private Rigidbody rigidbody;
  private bool animationRunning = false;
  private Vector3 playerOldPosition;
  private bool dead = false;

  private void Start() {
    CurrentHealth = MaxHealth;
    slider.maxValue = MaxHealth;
    slider.value = CurrentHealth;
    fightManager = GameObject.Find("GameManager").GetComponent<FightManager>();
    animator = GetComponent<Animator>();
    rigidbody = GetComponent<Rigidbody>();
  }

  public void SetPlayerOldPosition(Vector3 pos) {
    playerOldPosition = pos;
  }

  public void OnClick() {
    if (fightManager.Fighting() && !animationRunning && fightManager.currentTurn == FightManager.Turn.Player) {
      Attack();
    }
  }

  public void ReturnToOldPosition() {
    gameObject.transform.position = playerOldPosition;
  }

  public void Attack() {
    StartCoroutine(AttackPhase());
    fightManager.ButtonsActive(false);
  }

  public void CheckHealth() {
    if (CurrentHealth <= 0) {
      dead = true;
      fightManager.CanFight(false);
      ResetForNextFight();
      Debug.Log("Player is defeated");
    }
  }

  public void HealthRestored(int health) {
    if (CurrentHealth < MaxHealth) {
      CurrentHealth += health;
      slider.value = CurrentHealth;
    }
    if (CurrentHealth > 100) {
      CurrentHealth = 100;
    }
  }

  public void HealthDamaged(int damage) {
    if (CurrentHealth > 0) {
      CurrentHealth -= damage;
      slider.value = CurrentHealth;
      CheckHealth();
    }
  }

  private IEnumerator AttackPhase() {
    animationRunning = true;
    gameObject.GetComponent<SpriteRenderer>().enabled = false;
    fakePlayer.SetActive(true);
    animator = fakePlayer.GetComponent<Animator>();
    animator.Play("FakePlayer_Attacking");
    yield return new WaitForSeconds(0.85f);
    if (opponent == null) {
      opponent = fightManager.GetOpponent();
    }
    opponent.HealthDamaged(CurrentDamage);
    yield return new WaitForSeconds(1.65f);
    fakePlayer.SetActive(false);
    gameObject.GetComponent<SpriteRenderer>().enabled = true;
    animationRunning = false;
    fightManager.currentTurn = FightManager.Turn.Mob;
  }

  public void ResetForNextFight() {
    gameObject.GetComponent<PlayerMovement>().CanMove(true);
    CurrentHealth = MaxHealth;
    slider.value = CurrentHealth;
    ReturnToOldPosition();
    fightManager.ShowFightUI(false);
  }

}
