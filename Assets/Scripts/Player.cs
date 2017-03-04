using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Transform bufferPrefab;

    private Transform klein;
    private Mesh kleinMesh;
    private Material kleinMaterial;
    private Material canvasMat;
    private Slider colorSlider;
    private Toggle maxValueToggle;

    private float saturation;
    private Transform buffer;
    private Mesh bufferMesh;
    private Material bufferMaterial;


    void Start ()
    {
        klein = GameObject.Find("Klein").transform;
        kleinMesh = GameObject.Find("Klein").GetComponent<MeshFilter>().mesh;
        kleinMaterial = klein.GetComponent<MeshRenderer>().material;
        canvasMat = klein.GetComponent<MeshRenderer>().material;
        colorSlider = GameObject.Find("ColorSlider").GetComponent<Slider>();
        maxValueToggle = GameObject.Find("MaxValueToggle").GetComponent<Toggle>();

        // Init
        maxValueToggle.gameObject.SetActive(false);
        saturation = 1f;
        buffer = null;
        bufferMesh = null;
        bufferMaterial = null;
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

                if (buffer == null)
                {
                    if (colorSlider.value >= colorSlider.maxValue / 2f)
                    {
                        buffer = Instantiate(bufferPrefab, new Vector3(kleinMesh.bounds.max.x - (kleinMesh.bounds.max.x / 4f), kleinMesh.bounds.min.z + (kleinMesh.bounds.max.z / 4f), -1f), Quaternion.identity);
                        bufferMesh = buffer.GetComponent<MeshFilter>().mesh;
                        bufferMaterial = buffer.GetComponent<MeshRenderer>().material;
                        bufferMaterial.color = new Color(1f, 1f, 0f);
                    }
                }
            }
            else
            {
                saturation = 0.5f;
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (buffer != null)
            {
                if (mouseWorldPosition.x > buffer.position.x - (bufferMesh.bounds.extents.x)
                    && mouseWorldPosition.x < buffer.position.x + (bufferMesh.bounds.extents.x)
                    && mouseWorldPosition.y > buffer.position.y - (bufferMesh.bounds.extents.y)
                    && mouseWorldPosition.y < buffer.position.y + (bufferMesh.bounds.extents.y))
                {
                    bufferMaterial.color = kleinMaterial.color;
                }
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

        if (maxValueToggle.isOn && buffer != null)
        {
            Destroy(buffer.gameObject);
            buffer = null;
            bufferMesh = null;
            bufferMaterial = null;
        }
    }
}
