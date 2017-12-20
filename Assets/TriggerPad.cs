using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPad : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("PAD!!");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void onTriggerEnter(Collider other)
    {
        Debug.Log("So Triggered");
    }
}
