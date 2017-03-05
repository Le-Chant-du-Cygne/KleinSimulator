using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLines : MonoBehaviour {


    public Transform[] things;
    private LineRenderer lineRenderer;
    private Slider colorSlider;
    private Slider stateSlider;

    private int i;

	void Start () {

        things = GameObject.FindObjectsOfType<Transform>();
        lineRenderer = GetComponent<LineRenderer>();

        colorSlider = GameObject.Find("ColorSlider").GetComponent<Slider>();
        stateSlider = GameObject.Find("StateSlider").GetComponent<Slider>();
    }
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.A))
        {
            DrawLine();
        }



    }


   public void DrawLine()
    {

        if (i < 100)
        {
            lineRenderer.SetPosition(i, things[Random.Range(0, things.Length)].position);
            i++;
        }
        else
        {
            i = 0;
        }


    }

    public void SetLinesColor()
    {

        lineRenderer.startColor = new Color(colorSlider.value, Random.Range(colorSlider.value, 1f), Random.Range(0f, 1f));
        lineRenderer.endColor = new Color(Random.Range(0f, colorSlider.value), colorSlider.value, Random.Range(0f, colorSlider.value));
    }
}
