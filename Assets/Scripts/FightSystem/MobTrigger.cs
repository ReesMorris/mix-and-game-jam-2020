using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobTrigger : MonoBehaviour {

  public delegate void OnFightInitiate();
  public static OnFightInitiate onFightInitiate;

  private GameObject Player;
  private bool possibleFight = false;
  private FightManager fightManager;
  private InventoryManager inventoryManager;
  private bool hasSword;

  public TMPro.TMP_Text Text;
  public Transform PlayerFightSpawn;
  public Transform MobFightSpawn;
  public Item swordPrefab;

  private void Start() {
    InventoryManager.onInventoryChange += OnInventoryChange;
    fightManager = GameObject.Find("GameManager").GetComponent<FightManager>();
    inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    UpdateText();
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.F) && possibleFight && hasSword) {
      Player.GetComponent<PlayerFight>().SetPlayerOldPosition(Player.transform.position);
      transform.parent.GetComponent<Mob>().SetOldPosition(transform.position);
      if (onFightInitiate != null) onFightInitiate();
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

  private void UpdateText() {
    Text.text = "[F] Fight";
    if (!hasSword) Text.text += "<br><color=red>Requires Sword</color>";
  }

  private void OnInventoryChange(List<InventorySlot> items) {
    if (inventoryManager.ItemCount(swordPrefab) > 0) {
      hasSword = true;
      UpdateText();
    }
  }

  private void OnTriggerEnter(Collider other) {
    if (other.tag == "Player") {
      possibleFight = true;
      Text.gameObject.SetActive(true);
      Player = other.transform.gameObject;
    }
  }
  private void OnTriggerExit(Collider other) {
    possibleFight = false;
    Text.gameObject.SetActive(false);
  }
}
