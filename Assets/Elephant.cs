using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elephant : MonoBehaviour
{

    private GameObject elephant;

    void Start()
    {
        elephant = GameObject.Find("Elephant");
    }

    void Update()
    {

    }


    public void launchCoroutineElephant()
    {
        StartCoroutine(becomeAnElephant());
    }

    IEnumerator becomeAnElephant()
    {

        if (GetComponent<Toggle>().isOn)
        {
            elephant.GetComponent<SpriteRenderer>().enabled = true;
            elephant.GetComponent<AudioSource>().PlayScheduled(2);
        }

        yield return new WaitForSeconds(8f);

        elephant.GetComponent<SpriteRenderer>().enabled = false;

        yield return null;

    }

}
