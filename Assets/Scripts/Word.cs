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

        GetComponent<SpriteRenderer>().color = new Color(0.2f, Random.Range(0f, 1f), Random.Range(0f, 1f));

        Invoke("Destroy", 0.5f);
		
	}

    private void Destroy()
    {
        Destroy(gameObject);
    }

    void Update () {

        transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, 0) * -8 ;

	}
}
