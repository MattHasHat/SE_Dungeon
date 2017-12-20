using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour
{
	public List<Item> items = new List<Item> ();

	void Start ()
	{
		items.Add (new Item ("Coffee Cup", 0, "Your morning cup of joe!!"));
		items.Add (new Item ("Key", 1, "An oddly large golden key"));
		items.Add (new Item ("Map", 2, "Ye olde paper map"));
		items.Add (new Item ("Torch", 3, "It's a torch"));
	}
}
