using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicon : MonoBehaviour {

    private Transform player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter (Collider collider)
    {
        if (collider.gameObject.name == "Macron")
        {
            player.GetComponent<Player>().lart();
        }
    }
}
