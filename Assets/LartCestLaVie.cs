using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LartCestLaVie : MonoBehaviour {

    public string[] lart;
    public string[] cest;
    public string[] lavie;

    private Text lartText;
    private Text cestText;
    private Text lavieText;

    private float timer;
    private bool stopText;


    void Start () {

        lart = new string[] { "L'art", "L'arbre", "L'arabe", "L'arable", "L'or", "L'air", "Lard" };
        cest = new string[] { "c'est", "sait" };
        lavie = new string[] { "la vie", "l'avis", "lavis", "la vis", "le vice", "Levis", "Lévy", "le vit", "le oui" };

        lartText = GameObject.Find("Text_art").GetComponent<Text>();
        cestText = GameObject.Find("Text_cest").GetComponent<Text>();
        lavieText = GameObject.Find("Text_lavie").GetComponent<Text>();

    }
	
	void Update () {

        timer += Time.deltaTime;

        if (timer > 0.2 && !stopText)
        {
            lartText.text = lart[Random.Range(0, lart.Length)];
            cestText.text = cest[Random.Range(0, cest.Length)];
            lavieText.text = lavie[Random.Range(0, lavie.Length)];
            timer = 0;
        }



		
	}


    public void StopText()
    {
        stopText = true;
    }
    public void StartText()
    {
        stopText = false;
    }
}
