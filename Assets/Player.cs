using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Material canvasMat;
    private Slider colorSlider;
    private Toggle maxValueToggle;


    void Start ()
    {
        canvasMat = GameObject.Find("Klein").GetComponent<MeshRenderer>().material;
        colorSlider = GameObject.Find("ColorSlider").GetComponent<Slider>();
        maxValueToggle = GameObject.Find("MaxValueToggle").GetComponent<Toggle>();

        // Init
        maxValueToggle.gameObject.SetActive(false);
    }
	
	void Update ()
    {
		
	}

    public void setColor ()
    {
        HSVColor hsvColor = new HSVColor(colorSlider.value, 1f, 1f);
        canvasMat.color = hsvColor.ToColor();

        if (Mathf.Approximately(colorSlider.value, 0.5f))
        {
            maxValueToggle.gameObject.SetActive(true);
        }
    }

    public void setMaxValue ()
    {
        if (colorSlider.maxValue < 1f)
        {
            colorSlider.maxValue = 1f;
        }
        else
        {
            colorSlider.maxValue = 0.5f;
        }
    }
}
