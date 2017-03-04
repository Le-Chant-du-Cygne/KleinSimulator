using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRomain : MonoBehaviour {

    private Slider colorSlider;

    private float timer;
    private float timeWithoutMoving;
    private float blueKlein;


    void Start () {

        colorSlider = GameObject.Find("ColorSlider").GetComponent<Slider>();
        timeWithoutMoving = 5;
        blueKlein = 0.65f;
    }


    void Update () {

        timer += Time.deltaTime;

        if (timer >= timeWithoutMoving)
        {

            if (colorSlider.value < blueKlein -0.01f)
            {
                colorSlider.value += 0.01f;
            }
            else if (colorSlider.value > blueKlein + 0.01f)
            {
                colorSlider.value -= 0.01f;
            }
            else
            {
                timer = 0;
            }

        }

    }

}
