﻿using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Transform bufferPrefab;

    private PlayerRomain playerRomain;

    private Transform klein;
    private Mesh kleinMesh;
    private Material kleinMaterial;
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
    private Text sliderMoveDurationDisplay;
    private Text text;

    private float saturation;
    private Transform buffer;
    private Mesh bufferMesh;
    private Material bufferMaterial;
    private string[] texts;


    void Start ()
    {
        playerRomain = GetComponent<PlayerRomain>();

        klein = GameObject.Find("Klein").transform;
        kleinMesh = GameObject.Find("Klein").GetComponent<MeshFilter>().mesh;
        kleinMaterial = klein.GetComponent<MeshRenderer>().material;
        canvasMat = klein.GetComponent<MeshRenderer>().material;
        colorSlider = GameObject.Find("ColorSlider").GetComponent<Slider>();
        maxValueToggle = GameObject.Find("MaxValueToggle").GetComponent<Toggle>();
        sliderMoveDurationDisplay = GameObject.Find("SliderMoveDurationDisplay").GetComponent<Text>();
        text = GameObject.Find("Text").GetComponent<Text>();

        // Init
        maxValueToggle.gameObject.SetActive(false);
        saturation = 1f;
        buffer = null;
        bufferMesh = null;
        bufferMaterial = null;
        text.text = "";
        XmlDocument doc = new XmlDocument();
        doc.PreserveWhitespace = true;
        TextAsset xmlCorpus = Resources.Load<TextAsset>("corpusTextos");
        doc.LoadXml(xmlCorpus.text);
        if (doc.HasChildNodes && doc.ChildNodes.Count > 2)
        {
            XmlNodeList nodeList = doc.ChildNodes[2].ChildNodes;
            texts = new string[nodeList.Count];
            int index = 0;
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (nodeList[i].ChildNodes.Count > 2)
                {
                    texts[index] = nodeList[i].ChildNodes[3].InnerText;
                    index++;
                }
            }
        }
        Resources.UnloadAsset(xmlCorpus);
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
                    if (colorSlider.value >= colorSlider.maxValue / 2f && colorSlider.value >= colorSlider.maxValue / 2f + 0.5f)
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

        if (playerRomain.getTimerWithMoving() > 0)
        {
            text.text = texts[(int)(playerRomain.getTimerWithMoving() / 10f)];
            if (buffer != null)
            {
                text.color = bufferMaterial.color;
            }
        }
        else
        {
            text.text = "";
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
