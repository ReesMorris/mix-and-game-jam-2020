using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour {
  public NPC npcPrefab;
  public Sprite[] skins;
  public NPCWaypoints[] beforeShop;
  public NPCWaypoints[] afterShop;

  void Start() {
    SpawnNPC();
  }

  public void SpawnNPC() {
    NPC npc = Instantiate(npcPrefab);
    npc.Init(GetRandomWaypoints(beforeShop), GetRandomWaypoints(afterShop));
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


