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
  private AudioManager audioManager;

  private void Start() {
    CurrentHealth = MaxHealth;
    slider.maxValue = MaxHealth;
    slider.value = CurrentHealth;
    fightManager = GameObject.Find("GameManager").GetComponent<FightManager>();
    animator = GetComponent<Animator>();
    rigidbody = GetComponent<Rigidbody>();
    audioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
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
    if (opponent == null) {
      opponent = fightManager.GetOpponent(); // Get the opponent
    }
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
    // Disable main player render
    gameObject.GetComponent<SpriteRenderer>().enabled = false;

    // Activating animated model for attack
    fakePlayer.SetActive(true);

    // Starting animated model for attack
    animationRunning = true;
    animator = fakePlayer.GetComponent<Animator>();
    animator.Play("FakePlayer_Attacking");

    yield return new WaitForSeconds(0.85f);

    // Damaging the opponent
    opponent.HealthDamaged(CurrentDamage);

    // Play sound
    audioManager.PlaySound(fightManager.hitClip, true);

    yield return new WaitForSeconds(1.65f);

    // Animated player disabled
    fakePlayer.SetActive(false);

    // Main sprite re-enabled
    gameObject.GetComponent<SpriteRenderer>().enabled = true;

    // Stopping the animation
    animationRunning = false;

    //Updating the turn
    fightManager.currentTurn = FightManager.Turn.Mob;
  }

  public void ResetForNextFight() {
    gameObject.GetComponent<PlayerMovement>().CanMove(true); // Player able to move again
    CurrentHealth = MaxHealth; // Resetting player's health after fight
    slider.value = CurrentHealth; // Updating healthbar
    ReturnToOldPosition(); // Moving player back to old pos
    fightManager.ShowFightUI(false); // Hide UI
    opponent = null; // resetting opponent
  }

}
