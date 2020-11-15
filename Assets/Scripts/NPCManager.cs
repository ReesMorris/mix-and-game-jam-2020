using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour {
  public NPC npcPrefab;
  public Sprite[] skins;
  public NPCWaypoints[] beforeShop;
  public NPCWaypoints[] afterShop;

  void Start() {
    StartCoroutine(Spawner());
  }

  IEnumerator Spawner() {
    while (true) {
      yield return new WaitForSeconds(3f);
      SpawnNPC();
    }
  }

  public void SpawnNPC() {
    NPC npc = Instantiate(npcPrefab);
    npc.Init(GetRandomWaypoints(beforeShop), GetRandomWaypoints(afterShop), skins[Random.Range(0, skins.Length)]);

  }

  public List<Transform> GetRandomWaypoints(NPCWaypoints[] waypoints) {
    List<Transform> route = new List<Transform>();
    foreach (NPCWaypoints waypoint in waypoints) {
      int randomIndex = Random.Range(0, waypoint.waypoints.Length);
      route.Add(waypoint.waypoints[randomIndex]);
    }
    return route;
  }
}


