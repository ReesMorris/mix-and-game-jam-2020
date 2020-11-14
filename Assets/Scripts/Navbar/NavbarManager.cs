using UnityEngine;

public class NavbarManager : MonoBehaviour {

  private RectTransform currentMenu;
  private int hiddenPos = -999;
  private int visiblePos = 95;

  public void ToggleMenu(GameObject menu) {
    RectTransform menuTransform = menu.GetComponent<RectTransform>();

    if (menuTransform == currentMenu)
      if (currentMenu.anchoredPosition.y == hiddenPos) ShowMenu(currentMenu);
      else HideMenu(currentMenu);

    else {
      if (currentMenu)
        HideMenu(currentMenu);

      currentMenu = menuTransform;
      ShowMenu(currentMenu);
    }
  }


  void ShowMenu(RectTransform menu) {
    menu.anchoredPosition = new Vector2(menu.anchoredPosition.x, visiblePos);
  }

  void HideMenu(RectTransform menu) {
    menu.anchoredPosition = new Vector2(menu.anchoredPosition.x, hiddenPos);
  }
}
