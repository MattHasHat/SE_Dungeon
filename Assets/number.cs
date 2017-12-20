using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class number {

    public TextMesh tm;
    public int numb;

    void Start()
    {
        numb = 0;
        tm = (TextMesh)GameObject.Find("GameObject").GetComponent<TextMesh>();
        tm.text = "numb";
    }

    void Update()
    {
        Debug.Log("Working");
        tm.text = "1";
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Entered");
        if(tm.text == "0")
            tm.text = "1";
        if(tm.text == "1")
            tm.text = "2";
        if(tm.text == "2")
            tm.text = "3";
        if(tm.text == "3")
            tm.text = "4";
        if(tm.text == "4")
            tm.text = "5";
        if(tm.text == "5")
            tm.text = "6";
        if(tm.text == "6")
            tm.text = "7";
        if(tm.text == "7")
            tm.text = "8";
        if(tm.text == "8")
            tm.text = "9";
        if(tm.text == "9")
            tm.text = "0";
    }

}
