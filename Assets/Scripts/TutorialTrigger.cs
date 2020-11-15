using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour {

  public int tutorialSequence;

  private TutorialManager tutorialManager;

  void Start() {
    tutorialManager = GameObject.Find("GameManager").GetComponent<TutorialManager>();
  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag.Equals("Player")) {
      tutorialManager.OnPlayerCollision(tutorialSequence);
      Destroy(gameObject);
    }
  }
}
