using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MobTrigger : MonoBehaviour {

  private GameObject Player;
  private bool possibleFight = false;
  private FightManager fightManager;

  public TMPro.TMP_Text Text;
  public Transform PlayerFightSpawn;
  public Transform MobFightSpawn;

  private void Start() {
    fightManager = GameObject.Find("GameManager").GetComponent<FightManager>();
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.F) && possibleFight) {
      Player.GetComponent<PlayerFight>().SetPlayerOldPosition(Player.transform.position);
      transform.parent.GetComponent<Mob>().SetOldPosition(transform.position);
      TeleportFighters();
    }
  }

  private void TeleportFighters() {

    // Make sure player is facing right direction
    SpriteRenderer playerSprite = Player.GetComponent<SpriteRenderer>();
    if (playerSprite.flipX) playerSprite.flipX = false;

    // Positioning the player
    Player.transform.position = new Vector2(PlayerFightSpawn.transform.position.x, PlayerFightSpawn.transform.position.y);
    PlayerMovement pm = Player.GetComponent<PlayerMovement>();
    pm.CanMove(false); // player cannot move

    // positioning mob
    transform.parent.position = new Vector2(MobFightSpawn.transform.position.x, MobFightSpawn.transform.position.y);

    // ready to fight
    fightManager.CanFight(true);
    fightManager.SetOpponent(transform.parent.gameObject.GetComponent<Mob>()); // storing player's opponent

    // activate health bars
    fightManager.ShowFightUI(true);

  }

  private void OnTriggerEnter(Collider other) {
    if (other.tag == "Player") {
      possibleFight = true;
      Text.gameObject.SetActive(true);
      Player = other.transform.parent.gameObject;
    }
  }
  private void OnTriggerExit(Collider other) {
    possibleFight = false;
    Text.gameObject.SetActive(false);
  }
}
