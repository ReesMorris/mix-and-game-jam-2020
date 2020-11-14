[System.Serializable]
public class ChatMessage {

  public ChatMessage(string _message) {
    message = _message;
    delayBefore = 0;
  }
  public ChatMessage(string _message, float _delayBefore) {
    message = _message;
    delayBefore = _delayBefore;
  }

  public string message;
  public float delayBefore;
}
