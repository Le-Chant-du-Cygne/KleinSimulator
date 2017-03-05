using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AminDada : MonoBehaviour {


    public int mouseClicked;

    void Start () {

        mouseClicked = 0;

    }
	
	void Update () {


        if (Input.GetMouseButtonDown(0))
        {
            mouseClicked++;

        }

        if (mouseClicked > 500)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }

        if (mouseClicked > 501)
        {
            GetComponent<SpriteRenderer>().enabled = false;

        }

    }
}
