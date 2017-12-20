using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Player {

	public static Player current;
	private string name;
	private List<int> inventoryIDs;
	private int levelNumber;

	public Player () {
		name = "";
		inventoryIDs = null;
		levelNumber = 1;

	}

	public void setName(string name) {
		this.name = name;
	}

	public string getName() {
		return name;
	}

	public void setInventory(List<int> inventory) {
		this.inventoryIDs = inventory;
	}

	public List<int> getInventory() {
		return inventoryIDs;
	}

	public void setLevel(int levelNumber) {
		this.levelNumber = levelNumber;
	}

	public int getLevelNum() {
		return levelNumber;
	}
}
