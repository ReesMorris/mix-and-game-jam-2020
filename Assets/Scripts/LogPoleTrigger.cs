using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogPoleTrigger : MonoBehaviour {

  public Item axe;
  public TMPro.TMP_Text promptText;
  public GameObject chopped;
  public GameObject unchopped;
  public Pickupable pickupablePrefab;
  public Item log;
  public GameObject[] dropSpawns;
  public AudioClip cutSound;

  private InventoryManager inventoryManager;
  private bool hasAxe;
  private bool isChopped;
  private bool playerInRange;
  private AudioManager audioManager;

  void Start() {
    InventoryManager.onInventoryChange += OnInventoryChange;
    GetComponent<MeshRenderer>().enabled = false;
    inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    audioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
  }

  void Update() {
    if (Input.GetKeyUp(KeyCode.F)) {
      if (hasAxe && !isChopped && playerInRange) {
        isChopped = true;
        chopped.SetActive(true);
        unchopped.SetActive(false);
        audioManager.PlaySound(cutSound, true);
        UpdateText();
        DropItems();
        StartCoroutine(Respawn());
      }
    }
  }

  IEnumerator Respawn() {
    yield return new WaitForSeconds(60f);
    isChopped = false;
    chopped.SetActive(false);
    unchopped.SetActive(true);
    UpdateText();
  }

  void UpdateText() {
    if (isChopped) promptText.text = "";
    else {
      promptText.text = "[F] Chop";
      if (!hasAxe) promptText.text += "<br><color=red>Requires Axe</color>";
    }
  }

  private void OnTriggerEnter(Collider other) {
    UpdateText();
    if (other.gameObject.tag.Equals("Player")) {
      promptText.gameObject.SetActive(true);
      playerInRange = true;
    }
  }

  private void OnTriggerExit(Collider other) {
    if (other.gameObject.tag.Equals("Player")) {
      promptText.gameObject.SetActive(false);
      playerInRange = false;
    }
  }

  void OnInventoryChange(List<InventorySlot> items) {
    if (!hasAxe && inventoryManager.ItemCount(axe) > 0) hasAxe = true;
    UpdateText();
  }

  void DropItems() {
    List<GameObject> possiblePoints = new List<GameObject>(dropSpawns);
    int totalDrops = Random.Range(1, possiblePoints.Count - 1);

    for (int i = 0; i < totalDrops; i++) {
      int randomIndex = Random.Range(0, possiblePoints.Count - 1);
      GameObject point = possiblePoints[randomIndex];
      possiblePoints.RemoveAt(randomIndex);

      Pickupable item = Instantiate(pickupablePrefab);
      item.transform.position = point.transform.position;
      item.Init(log, Random.Range(1, 10));
    }
  }
}
