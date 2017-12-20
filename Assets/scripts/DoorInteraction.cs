using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour {

	public static int currentLevel;

	private Inventory InventoryComponent;

	// Use this for initialization
	void Start () {
		InventoryComponent = GameObject.FindGameObjectWithTag ("Player").GetComponent<Inventory>();
		currentLevel = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Door") && InventoryComponent.InventoryContains(1)) {
			//loadNextLevel ();
		}
	}

	void loadNextLevel() {
		switch(currentLevel) {
		case 1:
			currentLevel = 2;
			SceneManager.LoadScene ("level2");
			break;
		}
	}

	void reloadCurrentLevel() {
		switch (currentLevel) {
		case 1:
			SceneManager.LoadScene ("level1", LoadSceneMode.Single);
			break;
		case 2:
			SceneManager.LoadScene ("level2", LoadSceneMode.Single);
			break;
		}
	}
}
