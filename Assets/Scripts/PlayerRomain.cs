using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRomain : MonoBehaviour
{

    private Slider colorSlider;

    private float timer;
    private float timeWithoutMoving;
    private float blueKlein;

    /* Gael override #01 */
    float timerWithMoving = 0f;
    bool maxValueToggleHasBeenOn = false;
    Player player;
    private Toggle maxValueToggle;
    Text sliderMoveDurationDisplay;
    bool hasReachedTimeWithoutMoving = false;
    /**/

    void Start()
    {

        colorSlider = GameObject.Find("ColorSlider").GetComponent<Slider>();
        timeWithoutMoving = 5;
        blueKlein = 0.65f;

        /*G#01*/
        player = GetComponent<Player>();
        maxValueToggle = player.MaxValueToggle;
        sliderMoveDurationDisplay = GameObject.Find("SliderMoveDurationDisplay").GetComponent<Text>();
        sliderMoveDurationDisplay.text = "";
    }


    void Update()
    {
        /*G#01*/
        if(!maxValueToggleHasBeenOn && maxValueToggle.gameObject.activeSelf && maxValueToggle.isOn)
        {
            maxValueToggleHasBeenOn = true;
        }

        if(maxValueToggleHasBeenOn)
            timer += Time.deltaTime;

        if (timer >= timeWithoutMoving && player.getCurrentState() == Player.States.NORMAL)
        {
            if (colorSlider.value < blueKlein - 0.01f)
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

            if(colorSlider.value == colorSlider.maxValue || colorSlider.value == blueKlein)
            {
                hasReachedTimeWithoutMoving = true;
            }

        }

        /*G#01*/
        if (maxValueToggle.gameObject.activeSelf && !maxValueToggle.isOn && hasReachedTimeWithoutMoving)
        {
           // Debug.Log("yo");

            if (colorSlider.value != colorSlider.maxValue && colorSlider.value != blueKlein)
            {
                timerWithMoving += Time.deltaTime;
                sliderMoveDurationDisplay.text = timerWithMoving + " sec";
            }
            else
            {
                timerWithMoving = 0f;
                sliderMoveDurationDisplay.text = "";
            }
        }
        else
        {
            timerWithMoving = 0f;
            sliderMoveDurationDisplay.text = "";
        }
    }

    public float getTimerWithMoving ()
    {
        return timerWithMoving;
    }

}
