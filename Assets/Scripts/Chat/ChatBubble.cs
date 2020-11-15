using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubble : MonoBehaviour {

  public float textSpeed = 0.05f;
  public RectTransform chatContainer;
  public GameObject panel;
  public TMPro.TMP_Text displayText;
  public AudioClip audioClip;

  private List<ChatMessage> textQueue;
  private bool queueActive;
  private AudioManager audioManager;

  void Start() {
    audioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
  }

  void Update() {
    LookAtCamera();
  }

  // Sets the bubble text by commencing the coroutine
  public void QueueText(ChatMessage message) {
    if (textQueue == null) textQueue = new List<ChatMessage>();

    textQueue.Add(message);
    if (!queueActive) StartCoroutine(TypeText(message));
  }

  // Coroutine for typing animation
  private IEnumerator TypeText(ChatMessage chatMessage) {
    displayText.text = "";
    queueActive = true;

    yield return new WaitForSeconds(chatMessage.delayBefore);
    panel.SetActive(true);

    for (int i = 0; i < chatMessage.message.Length; i++) {
      displayText.text += chatMessage.message[i];
      audioManager.PlaySound(audioClip, true);
      yield return new WaitForSeconds(textSpeed);
    }

    yield return new WaitForSeconds(1f);

    textQueue.RemoveAt(0);
    panel.SetActive(false);

    if (textQueue.Count > 0) StartCoroutine(TypeText(textQueue[0]));
    else queueActive = false;

  }

  // Looks at the camera
  private void LookAtCamera() {
    transform.LookAt(2 * transform.position - Camera.main.transform.position);
  }
}
