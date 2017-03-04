using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trump : MonoBehaviour {

    public GameObject ball;
    private GameObject handle;

	void Start () {

        handle = GameObject.Find("Handle");

    }
	
	void Update () {


		
	}

   public void InstantiateBall()
    {
        GameObject myBall = Instantiate(ball, handle.transform.position, Quaternion.identity);
        myBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5, 6),2) * 120);

    }


}
