using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

  public ChatBubble playerChatBubble;
  public Item coinPrefab;
  public Item hatchetPrefab;
  public Item tablePrefab;
  public Item swordPrefab;

  private int tutorialIndex;
  private bool sequencePlaying = false;
  private PlayerMovement playerMovement;
  private InventoryManager inventoryManager;

  void Start() {
    tutorialIndex = 0;
    playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    inventoryManager = GameObject.Find("GameManager").GetComponent<InventoryManager>();

    NavbarManager.onNavbarMenuToggle += OnNavbarMenuToggle;
    CraftMenuItem.onCraftMenuItemPurchase += OnCraftMenuItemPurchase;
    BuildableTile.onTilePlaced += OnTilePlaced;
    TableTile.onItemAddedToTable += OnItemAddedToTable;
    MobTrigger.onFightInitiate += OnFightInitiate;

    StartCoroutine(Tutorial0());
  }

  public void OnPlayerCollision(int sequence) {
    if (sequence == 1 && tutorialIndex == 1) StartCoroutine(Tutorial1());
  }

  void OnNavbarMenuToggle(string name, bool isOpen) {
    if (tutorialIndex == 2 && name == "Build Menu" && isOpen && !sequencePlaying) StartCoroutine(Tutorial2());
    if (tutorialIndex == 3 && name == "Items Menu" && isOpen && !sequencePlaying) StartCoroutine(Tutorial3());
    if (tutorialIndex == 4 && name == "Craft Menu" && isOpen && !sequencePlaying) StartCoroutine(Tutorial4());
  }
  void OnCraftMenuItemPurchase(Item item) {
    if (tutorialIndex == 5 && item == hatchetPrefab) StartCoroutine(Tutorial5());
    if (tutorialIndex == 8 && item == swordPrefab) StartCoroutine(Tutorial8());
  }
  void OnTilePlaced(Item item) {
    if (tutorialIndex == 6 && item == tablePrefab && !sequencePlaying) StartCoroutine(Tutorial6());
  }
  void OnItemAddedToTable(Item item) {
    if (tutorialIndex == 7 && !sequencePlaying) StartCoroutine(Tutorial7());
  }
  void OnFightInitiate() {
    if (tutorialIndex == 9 && !sequencePlaying) StartCoroutine(Tutorial9());
  }

  IEnumerator Tutorial0() {
    playerMovement.CanMove(false);
    sequencePlaying = true;
    playerChatBubble.QueueText(new ChatMessage("Ahh, here I am at last."));
    playerChatBubble.QueueText(new ChatMessage("Ghost Island™!", 1));
    playerChatBubble.QueueText(new ChatMessage("I suppose I should start following my court order mandate.."));
    playerChatBubble.QueueText(new ChatMessage("The sooner I have built this shop, the sooner I can go home!", 1));
    playerChatBubble.QueueText(new ChatMessage("First thing's first, let's head north and go see the plot of land."));
    yield return new WaitForSeconds(22f);
    playerMovement.CanMove(true);
    tutorialIndex = 1;
    sequencePlaying = false;
  }

  IEnumerator Tutorial1() {
    sequencePlaying = true;
    yield return new WaitForSeconds(0.5f);
    playerMovement.CanMove(false);
    playerChatBubble.QueueText(new ChatMessage("Here it is! 90 square meters of pure, uninhabited land.", 1));
    playerChatBubble.QueueText(new ChatMessage("I suppose I should open my build tools in the lower-left corner to see what I can make."));
    tutorialIndex = 2;
    sequencePlaying = false;
  }

  IEnumerator Tutorial2() {
    sequencePlaying = true;
    playerMovement.CanMove(false);
    yield return new WaitForSeconds(0.5f);
    playerChatBubble.QueueText(new ChatMessage("Fantastic! This grid area represents where I can build. I can make anything!"));
    playerChatBubble.QueueText(new ChatMessage("Small issue, though.. I don't have any supplies!"));
    playerChatBubble.QueueText(new ChatMessage("The Judge did leave me some Coins to get started with..."));
    playerChatBubble.QueueText(new ChatMessage("Ah - here we go!"));
    yield return new WaitForSeconds(14f);
    inventoryManager.AddItemToInventory(coinPrefab, 10);
    yield return new WaitForSeconds(1f);
    playerChatBubble.QueueText(new ChatMessage("That should be in my inventory now. I'm (almost) rich!"));
    playerChatBubble.QueueText(new ChatMessage("Let's look at the Items menu in the lower-left corner to see what I have."));
    tutorialIndex = 3;
    sequencePlaying = false;
  }

  IEnumerator Tutorial3() {
    sequencePlaying = true;
    playerMovement.CanMove(false);
    yield return new WaitForSeconds(0.5f);
    playerChatBubble.QueueText(new ChatMessage("Here are my items! Anything I pick up will appear here."));
    playerChatBubble.QueueText(new ChatMessage("Most things I find can be sold in the shop.", 1));
    playerChatBubble.QueueText(new ChatMessage("Let's look at the last menu in the lower-left corner, crafting."));
    tutorialIndex = 4;
    sequencePlaying = false;
  }

  IEnumerator Tutorial4() {
    sequencePlaying = true;
    playerMovement.CanMove(false);
    yield return new WaitForSeconds(0.5f);
    playerChatBubble.QueueText(new ChatMessage("Ah, the ol' crafting menu."));
    playerChatBubble.QueueText(new ChatMessage("Items in this menu work slightly differently to buildable items."));
    playerChatBubble.QueueText(new ChatMessage("I can only craft one of each item, and they'll last forever. Decent, right?"));
    playerChatBubble.QueueText(new ChatMessage("We'll need some wood to make our shop - so let's invest in the axe."));
    tutorialIndex = 5;
    sequencePlaying = false;
  }

  IEnumerator Tutorial5() {
    sequencePlaying = true;
    playerMovement.CanMove(false);
    yield return new WaitForSeconds(0.5f);
    playerChatBubble.QueueText(new ChatMessage("Hey-hey, we have an axe! We can chop trees now."));
    playerChatBubble.QueueText(new ChatMessage("This will be more than enough to get started."));
    playerChatBubble.QueueText(new ChatMessage("Go chop some trees, get some wood, and start building the shop!"));
    playerChatBubble.QueueText(new ChatMessage("The most important thing we'll need is a table."));
    tutorialIndex = 6;
    yield return new WaitForSeconds(14.5f);
    playerMovement.CanMove(true);
    sequencePlaying = false;
  }

  IEnumerator Tutorial6() {
    sequencePlaying = true;
    playerMovement.CanMove(false);
    yield return new WaitForSeconds(0.5f);
    playerChatBubble.QueueText(new ChatMessage("Look how nice that table looks!"));
    playerChatBubble.QueueText(new ChatMessage("We're going to get customers very soon.. how exciting!"));
    playerChatBubble.QueueText(new ChatMessage("We'll need to put something actually on the table first, though.", 1));
    playerChatBubble.QueueText(new ChatMessage("Stand next to the table and left-click anything in your inventory that has a sale value."));
    playerChatBubble.QueueText(new ChatMessage("That'll add x1 of that item onto the table, but you can keep clicking."));
    yield return new WaitForSeconds(22f);
    tutorialIndex = 7;
    playerMovement.CanMove(true);
    sequencePlaying = false;
  }

  IEnumerator Tutorial7() {
    sequencePlaying = true;
    playerMovement.CanMove(true);
    yield return new WaitForSeconds(0.5f);
    playerChatBubble.QueueText(new ChatMessage("Niiiiice! This is going great!"));
    playerChatBubble.QueueText(new ChatMessage("We're going to have customers any minute now."));
    playerChatBubble.QueueText(new ChatMessage("And we'll be off this island in no time at all!"));
    playerChatBubble.QueueText(new ChatMessage("You don't need to be present for items to sell, but you will need to keep an eye on table stock."));
    playerChatBubble.QueueText(new ChatMessage("You should be able to find more valuable items around the map, so feel free to explore."));
    playerChatBubble.QueueText(new ChatMessage("Aim to craft a sword as soon as possible, as that'll let you get some super loot!"));
    yield return new WaitForSeconds(13f);
    tutorialIndex = 8;
    playerMovement.CanMove(true);
    sequencePlaying = false;
  }

  IEnumerator Tutorial8() {
    sequencePlaying = true;
    playerMovement.CanMove(false);
    yield return new WaitForSeconds(0.5f);
    playerChatBubble.QueueText(new ChatMessage("You've got a sword already? Nice!"));
    playerChatBubble.QueueText(new ChatMessage("This will let you fight monsters that live around the world."));
    playerChatBubble.QueueText(new ChatMessage("If you can defeat them, you'll get some great loot."));
    yield return new WaitForSeconds(11f);
    tutorialIndex = 9;
    playerMovement.CanMove(true);
    sequencePlaying = false;
  }

  IEnumerator Tutorial9() {
    sequencePlaying = true;
    yield return new WaitForSeconds(0.5f);
    playerChatBubble.QueueText(new ChatMessage("You've initiated a fight! If you win, you'll get some valuable items."));
    playerChatBubble.QueueText(new ChatMessage("You'll take it in turns to damage one another. You can flee at any time."));
    yield return new WaitForSeconds(11f);
    tutorialIndex = 10;
    sequencePlaying = false;
  }

}
