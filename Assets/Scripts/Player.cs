using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Transform klein;
    private Mesh kleinMesh;
    private Material canvasMat;
    private Slider colorSlider;
    private Toggle maxValueToggle;
    public Toggle MaxValueToggle
    {
        get
        {
            return maxValueToggle;
        }
    }

    private float saturation;


    void Start ()
    {
        klein = GameObject.Find("Klein").transform;
        kleinMesh = GameObject.Find("Klein").GetComponent<MeshFilter>().mesh;
        canvasMat = klein.GetComponent<MeshRenderer>().material;
        colorSlider = GameObject.Find("ColorSlider").GetComponent<Slider>();
        maxValueToggle = GameObject.Find("MaxValueToggle").GetComponent<Toggle>();

        // Init
        maxValueToggle.gameObject.SetActive(false);
        saturation = 1f;
    }
	
	void Update ()
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mouseWorldPosition.x > kleinMesh.bounds.max.x / 4f && mouseWorldPosition.x < kleinMesh.bounds.max.x &&
            mouseWorldPosition.y > kleinMesh.bounds.max.z / 2f && mouseWorldPosition.y < kleinMesh.bounds.max.z)
        {
            if (maxValueToggle.isOn)
            {
                saturation = 1f;
            }
            else
            {
                saturation = 0.5f;
            }
        }
	}

    public void setColor ()
    {
        HSVColor hsvColor = new HSVColor(colorSlider.value, saturation, 1f);
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
