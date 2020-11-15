using UnityEngine;

public class NavbarManager : MonoBehaviour {

  public delegate void OnNavbarMenuToggle(string name, bool visible);
  public static OnNavbarMenuToggle onNavbarMenuToggle;

  private RectTransform currentMenu;
  private int hiddenPos = -999;
  private int visiblePos = 95;
  private BuildableAreaManager buildableAreaManager;

  void Start() {
    buildableAreaManager = GameObject.Find("GameManager").GetComponent<BuildableAreaManager>();
  }

  public void ToggleMenu(GameObject menu) {
    RectTransform menuTransform = menu.GetComponent<RectTransform>();

    if (menuTransform == currentMenu) {
      if (currentMenu.anchoredPosition.y == hiddenPos) ShowMenu(currentMenu);
      else HideMenu(currentMenu);
    } else {
      if (currentMenu)
        HideMenu(currentMenu);
      currentMenu = menuTransform;
      ShowMenu(currentMenu);
    }
  }


  void ShowMenu(RectTransform menu) {
    menu.anchoredPosition = new Vector2(menu.anchoredPosition.x, visiblePos);
    if (onNavbarMenuToggle != null) onNavbarMenuToggle(menu.name, true);
    if (menu.name == "Build Menu")
      buildableAreaManager.SetGridVisible(true);
  }

  void HideMenu(RectTransform menu) {
    menu.anchoredPosition = new Vector2(menu.anchoredPosition.x, hiddenPos);
    if (onNavbarMenuToggle != null) onNavbarMenuToggle(menu.name, false);
    if (currentMenu.name == "Build Menu")
      buildableAreaManager.SetRecipe(null);
  }
}
