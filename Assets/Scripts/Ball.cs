using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject word;

    private Player player;
    private bool canPlay;
    

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        canPlay = false;
    }

    void Update()
    {

    }

    void OnCollisionEnter2D (Collision2D coll)
    {
        if (coll.transform.CompareTag("Ball"))
        {
            if (!coll.transform.GetComponent<Ball>().canPlay && !canPlay)
            {
                Instantiate(word, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                canPlay = true;
            }
            else
            {
                canPlay = false;
                coll.transform.GetComponent<Ball>().canPlay = false;
            }
        }
        else if (coll.transform.CompareTag("KleinColliders"))
        {
            player.showFrame();

            if (coll.gameObject.name == "Top")
            {
                player.offsetFrameBottom();
            }
            else if (coll.gameObject.name == "Bottom")
            {
                player.offsetFrameTop();
            }
            else if (coll.gameObject.name == "Left")
            {
                player.offsetFrameRight();
            }
            else if (coll.gameObject.name == "Right")
            {
                player.offsetFrameLeft();
            }
        }
    }
}
