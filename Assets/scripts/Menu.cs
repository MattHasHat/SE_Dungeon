using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	// Set in UnityEditor
	public int buffer;

	// Static allows for other game mechanics to be "paused" while menu is used
	// Determines if MainMenu is active
	public static bool showMenu;

	// Determines if SubMenu is active
	private bool showEndGameMenu;
	private bool showControlsMenu;
	private bool showLoadMenu;
	private bool showSaveMenu;
	private bool showTextField;
	private string newSaveName;
	private int currentIndex;
	private Inventory InventoryComponent;

	public Vector2 scrollPosition = Vector2.zero;

	void Start () {
		Player.current = new Player ();

		showMenu = false;
		showEndGameMenu = false;
		showControlsMenu = false;
		showLoadMenu = false;
		showSaveMenu = false;
		showTextField = false;
		newSaveName = "";
		currentIndex = -1;

		InventoryComponent = GameObject.FindGameObjectWithTag ("Player").GetComponent<Inventory>();
	}

	void Update () {
		if (Input.GetButtonDown ("Menu")) {
			showMenu = !showMenu;
			showEndGameMenu = false;
			showControlsMenu = false;
			showLoadMenu = false;
			showSaveMenu = false;
			showTextField = false;
		}
	}

	void OnGUI () {
		if (showMenu) {

			int margin = (Screen.height - (buffer * 2) - 35) / 5;

			// Make a background box for all menus
			GUI.Box (new Rect (buffer, buffer, Screen.width - (buffer * 2), Screen.height - (buffer * 2)), "Menu");

			// Show the Main Menu buttons whenever none of the subMenus are not active
			if (!showEndGameMenu && !showControlsMenu && !showLoadMenu && !showSaveMenu) {
				
				// Makes 5 Main Menu buttons for player to 
				if (GUI.Button (new Rect (buffer * 2, buffer + 35, Screen.width - (buffer * 4), margin - 10), "Save Game")) {
					showSaveMenu = true;
				}
				if (GUI.Button (new Rect (buffer * 2, buffer + 35 + margin, Screen.width - (buffer * 4), margin - 10), "Load Game")) {
					showLoadMenu = true;
				}
				if (GUI.Button (new Rect (buffer * 2, buffer + 35 + margin * 2, Screen.width - (buffer * 4), margin - 10), "Controls")) {
					showControlsMenu = true;
				}
				if (GUI.Button (new Rect (buffer * 2, buffer + 35 + margin * 3, Screen.width - (buffer * 4), margin - 10), "Quit Game")) {
					showEndGameMenu = true;
				}
				if (GUI.Button (new Rect (buffer * 2, buffer + 35 + margin * 4, Screen.width - (buffer * 4), margin - 10), "Exit Menu")) {
					showMenu = false;
				}

				// Show the EndGame Menu to prompt player to confirm quiting game
			} else if (showEndGameMenu) {
				
				GUI.Box (new Rect ((Screen.width / 2) - 100, (Screen.height / 2) - 60, 200, 120), "Want to Quit?");
				if (GUI.Button (new Rect ((Screen.width / 2) - 93, (Screen.height / 2), 90, 55), "No")) {
					showEndGameMenu = false;
				}
				if (GUI.Button (new Rect ((Screen.width / 2) + 3, (Screen.height / 2), 90, 55), "Yes")) {
					Application.Quit();
					//UnityEditor.EditorApplication.isPlaying = false;
				}
				// Display a list of all game controls
			} else if (showControlsMenu) {
				if (GUI.Button (new Rect (buffer + 25, buffer + 25, 50, 50), "Back")) {
					showControlsMenu = false;
				}
				scrollPosition = GUI.BeginScrollView (new Rect (buffer + 80, buffer + 25, Screen.width - (buffer * 2) - 80, Screen.height - (buffer * 2) - 25), scrollPosition, new Rect (0, 0, Screen.width - (buffer * 2) - 100, 500));

				float middleScrollViewX = (Screen.width - (buffer * 2) - 100) / 2;

				GUI.Label (new Rect (middleScrollViewX / 2, 20, 100, 50), "Action");
				GUI.Label (new Rect (middleScrollViewX * 4 / 3, 20, 100, 50), "Keys");

				GUI.Label (new Rect (middleScrollViewX / 2 - 12, 100, 100, 50), "Movement");

				GUI.Box (new Rect (middleScrollViewX * 4 / 3 - 90, 115, 25, 25), "a");
				GUI.Box (new Rect (middleScrollViewX * 4 / 3 - 60, 115 - 30, 25, 25), "w");
				GUI.Box (new Rect (middleScrollViewX * 4 / 3 - 60, 115, 25, 25), "s");
				GUI.Box (new Rect (middleScrollViewX * 4 / 3 - 30, 115, 25, 25), "d");

				GUI.Label (new Rect (middleScrollViewX * 4 / 3 + 5, 100, 100, 50), "or");

				GUI.Box (new Rect (middleScrollViewX * 4 / 3 + 25, 115, 37, 25), "left");
				GUI.Box (new Rect (middleScrollViewX * 4 / 3 + 65, 115 - 30, 37, 25), "up");
				GUI.Box (new Rect (middleScrollViewX * 4 / 3 + 65, 115, 37, 25), "down");
				GUI.Box (new Rect (middleScrollViewX * 4 / 3 + 105, 115, 37, 25), "right");


				GUI.Label (new Rect (middleScrollViewX / 2 - 35, 180, 150, 50), "Access Inventory");
				GUI.Box (new Rect (middleScrollViewX * 4 / 3, 180, 25, 25), "i");

				GUI.Label (new Rect (middleScrollViewX / 2, 240, 150, 50), "Sprint");
				GUI.Box (new Rect (middleScrollViewX * 4 / 3 - 22, 240, 70, 25), "Left Shift");

				GUI.Label (new Rect (middleScrollViewX / 2, 300, 150, 50), "Jump");
				GUI.Box (new Rect (middleScrollViewX * 4 / 3 - 12, 300, 50, 25), "Space");

				GUI.EndScrollView ();
			} else if (showSaveMenu) {
				
				SaveLoad.Load();
				string[] savedNames = new string[3];
				bool[] isNotNull = new bool[3];

				for (int i = 0; i < 3; i++) {
					if (SaveLoad.savedGames.Count > i) {
						savedNames[i] = SaveLoad.savedGames [i].getName ();
						isNotNull[i] = true;
					} else {
						savedNames[i] = "Empty Savefile " + i;
						isNotNull[i] = false;
					}
				}

				int submenuMargin = (Screen.height - (buffer * 2) - 35) / 4;

				if (!showTextField) {
					if (GUI.Button (new Rect (buffer * 2, buffer + 35, Screen.width - (buffer * 4), submenuMargin - 10), savedNames [0])) {
						currentIndex = 0;
						if (!isNotNull [0]) {
							newSaveName = "Enter Name Here...";
							showTextField = true;
						}
					}
					if (GUI.Button (new Rect (buffer * 2, buffer + 35 + submenuMargin, Screen.width - (buffer * 4), submenuMargin - 10), savedNames [1])) {
						currentIndex = 1;
						if (!isNotNull [1]) {
							newSaveName = "Enter Name Here...";
							showTextField = true;
						}
					}
					if (GUI.Button (new Rect (buffer * 2, buffer + 35 + submenuMargin * 2, Screen.width - (buffer * 4), submenuMargin - 10), savedNames [2])) {
						currentIndex = 2;
						if (!isNotNull [2]) {
							newSaveName = "Enter Name Here...";
							showTextField = true;
						}
					}
				} else {
					newSaveName = GUI.TextField (new Rect (Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), newSaveName, 20);
				}
				if (GUI.Button (new Rect (Screen.width / 2 - 85, buffer + 35 + submenuMargin * 3, 50, 50), "Back")) {
					showSaveMenu = false;
					showTextField = false;
					currentIndex = -1;
				}
				if (GUI.Button (new Rect (Screen.width / 2 - 25, buffer + 35 + submenuMargin * 3, 50, 50), "Delete")) {
					if (currentIndex != -1) {
						SaveLoad.savedGames.RemoveAt (currentIndex);
						SaveLoad.Save ();
						currentIndex = -1;
					}
				}
				if (GUI.Button (new Rect (Screen.width / 2 + 35, buffer + 35 + submenuMargin * 3, 50, 50), "Save")) {
					if (currentIndex != -1) {
						Player.current.setName (newSaveName);
						Player.current.setInventory (getItemIDs(InventoryComponent.inventory));
						SaveLoad.AddSaveFile ();
						SaveLoad.Save();

						showTextField = false;
						showSaveMenu = false;
						showEndGameMenu = true;
						currentIndex = -1;
					}
				}

			} else if (showLoadMenu) {

				SaveLoad.Load();
				string[] savedNames = new string[3];
				bool[] isNotNull = new bool[3];

				for (int i = 0; i < 3; i++) {
					if (SaveLoad.savedGames.Count > i) {
						savedNames[i] = SaveLoad.savedGames [i].getName ();
						isNotNull[i] = true;
					} else {
						savedNames[i] = "Empty Savefile " + i;
						isNotNull[i] = false;
					}
				}

				int submenuMargin = (Screen.height - (buffer * 2) - 35) / 4;

				if (GUI.Button (new Rect (buffer * 2, buffer + 35, Screen.width - (buffer * 4), submenuMargin - 10), savedNames[0])) {
					if (isNotNull [0]) {
						Player.current = SaveLoad.savedGames [0];
						for (int i = 0; i < Player.current.getInventory().Count; i++) {
							InventoryComponent.AddItem (Player.current.getInventory()[i]);
						}
					}
				}
				if (GUI.Button (new Rect (buffer * 2, buffer + 35 + submenuMargin, Screen.width - (buffer * 4), submenuMargin - 10), savedNames[1])) {
					if (isNotNull [1]) {
						Player.current = SaveLoad.savedGames [1];
						for (int i = 0; i < Player.current.getInventory().Count; i++) {
							InventoryComponent.AddItem (Player.current.getInventory()[i]);
						}
					}
				}
				if (GUI.Button (new Rect (buffer * 2, buffer + 35 + submenuMargin * 2, Screen.width - (buffer * 4), submenuMargin - 10), savedNames[2])) {
					if (isNotNull [2]) {
						Player.current = SaveLoad.savedGames [2];
						for (int i = 0; i < Player.current.getInventory().Count; i++) {
							InventoryComponent.AddItem (Player.current.getInventory()[i]);
						}
					}
				}
				if (GUI.Button (new Rect (Screen.width / 2 - 25, buffer + 35 + submenuMargin * 3, 50, 50), "Back")) {
					showLoadMenu = false;
				}
			}
		}
	}

	private List<int> getItemIDs(List<Item> inventory) {
		List<int> itemIDs = new List<int>();
		for (int i = 0; i < inventory.Count; i++) {
			if (inventory [i].itemName != null && inventory[i].itemName.Length>0) {
				itemIDs.Add (inventory [i].itemID);
			}
		}
		return itemIDs;
	}

}
