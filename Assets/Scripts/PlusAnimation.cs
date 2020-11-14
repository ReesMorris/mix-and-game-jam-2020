using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusAnimation : MonoBehaviour {

  private Animator animator;

  void Start() {
    animator = GetComponent<Animator>();
    animator.Play("Plus Animation");
    StartCoroutine(Delete());
  }

  public void SetText(string message) {
    TMPro.TMP_Text text = GetComponent<TMPro.TMP_Text>();
    text.text = message;
  }

  IEnumerator Delete() {
    yield return new WaitForSeconds(1f);
    Destroy(gameObject);
  }

}
