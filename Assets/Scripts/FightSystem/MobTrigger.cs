using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MobTrigger : MonoBehaviour {

  private GameObject Mob;
  private bool possibleFight = false;

  public TMPro.TMP_Text Text;
  public Transform PlayerFightSpawn;
  public Transform MobFightSpawn;
  public bool Fighting = false;

  private void Update() {
    if (Input.GetKeyDown(KeyCode.F) && possibleFight) {
      TeleportFighters();
    }
  }

  private void TeleportFighters() {
    SpriteRenderer playerSprite = transform.parent.gameObject.GetComponent<SpriteRenderer>();
    if (playerSprite.flipX) playerSprite.flipX = false;
    transform.parent.position = new Vector2(PlayerFightSpawn.transform.position.x, PlayerFightSpawn.transform.position.y);
    Mob.transform.position = new Vector2(MobFightSpawn.transform.position.x, MobFightSpawn.transform.position.y);
    Fighting = true;

  }

  private void OnTriggerEnter(Collider other) {
    if (other.tag == "Mob") {
      possibleFight = true;
      Text.gameObject.SetActive(true);
      Mob = other.transform.parent.gameObject;
    }
  }
  private void OnTriggerExit(Collider other) {
    possibleFight = false;
    Text.gameObject.SetActive(false);
  }
}
