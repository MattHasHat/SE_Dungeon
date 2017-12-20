using UnityEngine;
using System.Collections;

public class playerPickUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coffeeCup"))
        {
            GetComponent<Inventory>().AddItem(0);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("key"))
        {
            GetComponent<Inventory>().AddItem(1);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("map"))
        {
            GetComponent<Inventory>().AddItem(2);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("torch"))
        {
            GetComponent<Inventory>().AddItem(3);
            other.gameObject.SetActive(false);
        }
    }
}
