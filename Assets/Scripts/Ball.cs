using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {


    public GameObject word;
    private bool canPlay;

	void Start () {
        canPlay = false;
	}
	
	void Update () {
		
	}



    void OnCollisionEnter2D(Collision2D coll)
    {


            if (coll.transform.CompareTag("Ball"))
            {

            if (!coll.transform.GetComponent<Ball>().canPlay && !canPlay)
            {
                Instantiate(word, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                canPlay = true;
            }else
            {
                canPlay = false;
                coll.transform.GetComponent<Ball>().canPlay = false;
            }



            }


    }
}
