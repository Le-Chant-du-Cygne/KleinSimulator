using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Word : MonoBehaviour {

    public Sprite[] words;
    public AudioClip[] clips;

    void Start () {

        words = Resources.LoadAll<Sprite>("Words");
        clips = Resources.LoadAll<AudioClip>("Clips");
       int randomNumber = Random.Range(0, words.Length);
        GetComponent<SpriteRenderer>().sprite = words[randomNumber];
        GetComponent<AudioSource>().PlayOneShot(clips[randomNumber]);
		
	}



	void Update () {
		
	}
}
