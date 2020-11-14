using UnityEngine;

public class NavbarItem : MonoBehaviour {

  public enum QuickKeyCode { B, C, I }
  public GameObject associatedMenu;
  public QuickKeyCode keyCode;

  private NavbarManager navbarManager;

  void Start() {
    navbarManager = transform.parent.GetComponent<NavbarManager>();
  }

  void Update() {
    if (Input.GetKeyDown(keyCode.ToString().ToLower())) {
      OnClick();
    }
  }

  public void OnClick() {
    navbarManager.ToggleMenu(associatedMenu);
  }
}