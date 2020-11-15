using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuItem : MonoBehaviour {

  public Animator mainMenu;

  private bool started;
  private TutorialManager tutorialManager;
  private TMPro.TMP_Text text;

  void Start() {
    tutorialManager = GameObject.Find("GameManager").GetComponent<TutorialManager>();
    text = GetComponent<TMPro.TMP_Text>();
  }

  void Update() {
    if (!started && Input.anyKeyDown) {
      started = true;
      StartCoroutine(FadeOutMenu());
    }
  }

  IEnumerator FadeOutMenu() {
    mainMenu.Play("FadeOut");
    text.text = "";
    yield return new WaitForSeconds(1f);
    tutorialManager.StartTutorial();
    yield return new WaitForSeconds(1f);
    Destroy(mainMenu.gameObject);
  }
}
