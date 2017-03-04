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
    public SpriteRenderer wordSprite;
    public Color wordSpriteColor;

    private Slider colorSlider;
    private Player player;
    private Toggle maxValueToggle;

    void Start()
    {
        wordSprite.enabled = false;
        //wordSpriteGo.SetActive(false);

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
                wordSpriteColor = new HSVColor(0.65f, 1f, 1f).ToColor();
            }
            else
            {
                wordSpriteColor = new HSVColor(0.5f, 1f, 1f).ToColor();
            }

            wordSprite.color = wordSpriteColor;
        }
    }

    private void init()
    {
        activated = true;
        fadeInTimer = 0f;
        wordSprite.enabled = true;
        wordSprite.color = wordSpriteColor;
        //wordSpriteGo.SetActive(true);
        //wordSpriteMaterial.color = wordSpriteColor;
        initialized = true;
    }
}
