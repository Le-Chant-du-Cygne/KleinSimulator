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
    bool buttonShowed = false;
    public Button btn;
    public bool btnActivated;
    int btnId = 0;
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

        hideButton();
    }


    void Update()
    {
        if (buttonShowed)
        {
            animateButton();
        }

        /*G#01*/
        if (!maxValueToggleHasBeenOn && maxValueToggle.gameObject.activeSelf && maxValueToggle.isOn)
        {
            maxValueToggleHasBeenOn = true;
        }

        if (maxValueToggleHasBeenOn)
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

            if (colorSlider.value == colorSlider.maxValue || colorSlider.value == blueKlein)
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
                if (player.hasReachedRotation && player.State == Player.States.NORMAL)
                {
                    sliderMoveDurationDisplay.text = "BINGO !!!";
                    //Then condition
                    if (!buttonShowed)
                    {
                        showButton();
                    }
                }
                else
                {
                    if (buttonShowed)
                    {
                        hideButton();
                    }
                    sliderMoveDurationDisplay.text = timerWithMoving + " sec";
                }

            }
            else
            {
                if (buttonShowed)
                {
                    hideButton();
                }
                timerWithMoving = 0f;
                sliderMoveDurationDisplay.text = "";
            }
        }
        else
        {
            if (buttonShowed)
            {
                hideButton();
            }
            timerWithMoving = 0f;
            sliderMoveDurationDisplay.text = "";
        }
    }

    public float getTimerWithMoving()
    {
        return timerWithMoving;
    }

    void showButton()
    {
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-5f, 5f);
        btn.transform.position = new Vector3(x, y, -2f);
        buttonShowed = true;
    }

    void hideButton()
    {
        btn.transform.position = new Vector3(-100, 0, 0);
        buttonShowed = false;
    }

    void animateButton()
    {
        if (btnActivated)
        {
            string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            btn.GetComponentInChildren<Text>().text = letters[btnId];
        }
        else
        {
            btn.GetComponentInChildren<Text>().text = (btnId % 2) == 0 ? "" : "claque ici!";
            ColorBlock colors = btn.colors;
            colors.normalColor = new HSVColor((float)btnId / 26f, 1f, 1f).ToColor();
            btn.colors = colors;
        }
        btnId++;
        if (btnId >= 26)
        {
            btnId = 0;
        }
    }
}
