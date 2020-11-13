using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubble : MonoBehaviour {

  public float textSpeed = 0.05f;
  public RectTransform chatContainer;
  public TMPro.TMP_Text displayText;

  void Start() {
    SetText("Why did I move here? I guess it was the weather.");
  }

  void Update() {
    LookAtCamera();
  }

  // Sets the bubble text by commencing the coroutine
  public void SetText(string message) {
    ClearText();
    StartCoroutine(TypeText(message));
  }

  // Clears current bubble text, but does not hide the bubble
  public void ClearText() {
    displayText.text = "";
  }

  // Coroutine for typing animation
  private IEnumerator TypeText(string message) {
    for (int i = 0; i < message.Length; i++) {
      displayText.text += message[i];
      yield return new WaitForSeconds(textSpeed);
    }
  }

  // Looks at the camera
  private void LookAtCamera() {
    transform.LookAt(2 * transform.position - Camera.main.transform.position);
  }
}
