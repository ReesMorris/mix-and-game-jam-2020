using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

  public GameObject Player;
  public Transform CameraFightPos;

  private FightManager fightManager;

  private void Start() {
    fightManager = GameObject.Find("GameManager").GetComponent<FightManager>();
  }

  private void Update() {
    if (fightManager != null && fightManager.Fighting()) {
      transform.position = new Vector3(CameraFightPos.position.x, CameraFightPos.position.y, transform.position.z);
    } else {
      transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
    }
  }
}
