using UnityEngine;

public class NavbarItem : MonoBehaviour {

  public GameObject associatedMenu;

  private NavbarManager navbarManager;

  void Start() {
    navbarManager = transform.parent.GetComponent<NavbarManager>();
  }

  public void OnClick() {
    navbarManager.ToggleMenu(associatedMenu);
  }
}