using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColoredWordDisplay : MonoBehaviour {

    public bool condition = false;
    private bool activated = false;
    private bool initialized = false;

    public float fadeInDuration = 1f;
    private float fadeInTimer = 0f;

    // public GameObject wordSpriteGo;
    public Text title;
    public Color titleColor;

    private Slider colorSlider;
    private Player player;
    private Toggle maxValueToggle;

    private string[] words = {  "YES", "NO", "FUCK", "MAIS", "OOH!", "BAT", "GUY", "MAN", "SOSO", "TRIC", "TRAC", "LARD", "NANA", "ANTI", "CHIP", "GARS",
                                "VIENS", "PAS", "DANS", "SA", "MON", "CUL", "PARS", "TUE", "LA", "TARD", "AVEC", "ELLE", "LUI", "MENT", "TROP", "SENS", "SANS",
        "VIE", "LE", "TA", "SA", "SON", "MON", "EST", "TU", "JE", "MOTO", "IL", "EUX", "PARS", "QUE", "PAPA", "BABA", "CACA", "FIFI", "CHUT", "BUT", "FIUT", "$$$",
        "KENT", "LOLA", "LOL", "VIET"};

    void Start()
    {
        title.enabled = false;

        colorSlider = GameObject.Find("ColorSlider").GetComponent<Slider>();
        player = GetComponent<Player>();
        maxValueToggle = player.MaxValueToggle;
    }

    void Update()
    {
        if (condition && !initialized)
        {
            init();
        }

        if (activated && maxValueToggle.enabled)
        {
            if(maxValueToggle.isOn)
            {
                titleColor = new HSVColor(0.65f, 1f, 1f).ToColor();
            }
            else
            {
                titleColor = new HSVColor(0.5f, 1f, 1f).ToColor();
            }

            title.color = titleColor;
        }
    }

    private void init()
    {
        activated = true;
        fadeInTimer = 0f;
        title.enabled = true;
        title.color = titleColor;
        initialized = true;
    }

    public void changeWord()
    {
        int i = Mathf.FloorToInt(Random.Range(0, words.Length));
        title.text = words[i];
    }
}
