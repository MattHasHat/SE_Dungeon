using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
	public int width;
	public int height;
	public GUISkin skin;
	private bool showInventory;
	private bool showPopUp;
	private bool draggingItem;
	private int draggedIndex;
	private string popUp;
	public List<Item> inventory = new List<Item> ();
	public List<Item> grid = new List<Item> ();
	private ItemDatabase database;
	private Item draggedItem;

	void Start ()
	{
		for (int i = 0; i < (width * height); i++) {
			grid.Add (new Item ());
			inventory.Add (new Item ());
		}
		database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDatabase> ();
	}

	

	void Update ()
	{
		if (Input.GetButtonDown ("Inventory")) {
			showInventory = !showInventory;
		}
	}

	void OnGUI ()
	{
		GUI.skin = skin;
		popUp = "";
		if (showInventory) {
			DrawInventory ();
			if (showPopUp) {
				GUI.Box (new Rect (Event.current.mousePosition.x, Event.current.mousePosition.y, 100, 50), popUp, skin.GetStyle ("popup"));
			}
		}
		if (draggingItem) {
			GUI.Box (new Rect (Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.itemIcon);
		}
	}

	void DrawInventory ()
	{
		Event e = Event.current;
		int i = 0;
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				Rect gridRect = new Rect (x * 60, y * 60, 50, 50);
				GUI.Box (gridRect, "", skin.GetStyle ("space"));
				grid [i] = inventory [i];
				if (grid [i].itemName!=null && grid[i].itemName.Length>0) {
					GUI.DrawTexture (gridRect, grid [i].itemIcon);
					if (gridRect.Contains (e.mousePosition)) {
						popUp = CreatePopUp (grid [i]);
						showPopUp = true;
						if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem) {
							draggingItem = true;
							draggedIndex = i;
							draggedItem = grid [i];
							inventory [i] = new Item ();
						}
						if (e.type == EventType.mouseUp && draggingItem) {
							inventory [draggedIndex] = inventory [i];
							inventory [i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
					}
				} else {
					if (gridRect.Contains (e.mousePosition)) {
						if (e.type == EventType.mouseUp && draggingItem) {
							inventory [i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
					}
				}
				if (popUp == "") {
					showPopUp = false;
				}
				i++;
			}
		}
	}

	string CreatePopUp (Item item)
	{
		popUp = "<color= #ffffff>" + item.itemName + "\n" + item.itemDescription + "</color>";
		return popUp;
	}

	public void AddItem (int id)
	{
		for (int i = 0; i < inventory.Count; i++) {
			if (inventory [i].itemName == null || inventory[i].itemName.Length<1) {
				for (int j = 0; j < database.items.Count; j++) {
					if (database.items [j].itemID == id) {
						inventory [i] = database.items [j];
					}
				}
				break;
			}
		}
	}

	void RemoveItem (int id)
	{
		for (int i = 0; i < inventory.Count; i++) {
			if (inventory [i].itemID == id) {
				inventory [i] = new Item ();
				break;
			}
		}
	}

	public bool InventoryContains (int id)
	{
		bool contains = false;
		for (int i = 0; i < inventory.Count; i++) {
			contains = (inventory [i].itemID == id);
			if (contains) {
				break;
			}
		}
		return contains;
	}
}
