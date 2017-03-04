using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Material canvasMat;
    private Slider colorSlider;


	void Start ()
    {
        canvasMat = GameObject.Find("Klein").GetComponent<MeshRenderer>().material;
        colorSlider = GameObject.Find("ColorSlider").GetComponent<Slider>();
	}
	
	void Update ()
    {
		
	}

    public void setColor ()
    {
        HSVColor hsvColor = new HSVColor(colorSlider.value, 1f, 1f);

        canvasMat.color = hsvColor.ToColor();
    }
}
