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
      TeleportFighters();
    }
  }

  private void TeleportFighters() {
    SpriteRenderer playerSprite = Player.GetComponent<SpriteRenderer>();
    if (playerSprite.flipX) playerSprite.flipX = false;
    Player.transform.position = new Vector2(PlayerFightSpawn.transform.position.x, PlayerFightSpawn.transform.position.y);
    PlayerMovement pm = Player.GetComponent<PlayerMovement>();
    pm.CanMove(false);
    transform.parent.position = new Vector2(MobFightSpawn.transform.position.x, MobFightSpawn.transform.position.y);
    fightManager.CanFight(true);

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
