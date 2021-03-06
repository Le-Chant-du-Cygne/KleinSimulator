﻿using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Transform bufferPrefab;

    public enum States { NORMAL, ROTATION, SCALE }

    private PlayerRomain playerRomain;

    private Transform klein;
    private Mesh kleinMesh;
    private Material kleinMaterial;
    private Transform frame;
    private Material frameMaterial;
    private Transform macron;
    private Transform chicon;
    private Text coloredTitle;
    private Material canvasMat;
    private Slider colorSlider;
    private Slider stateSlider;
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
    private States state;
    public States State
    {
        get
        {
            return state;
        }
    }
    private int originalColoredTitleFontSize;
    private int maxColoredTitleSizeDiff;

    private ColoredWordDisplay coloredWordDisplay;

    private Text lartText;
    private Text cestText;
    private Text lavieText;
    private MeshRenderer mr;
    public bool hasReachedRotation = false;


    void Start()
    {
        playerRomain = GetComponent<PlayerRomain>();

        klein = GameObject.Find("Klein").transform;
        kleinMesh = GameObject.Find("Klein").GetComponent<MeshFilter>().mesh;
        kleinMaterial = klein.GetComponent<MeshRenderer>().material;
        mr = klein.GetComponent<MeshRenderer>();
        frame = GameObject.Find("Frame").transform;
        frameMaterial = frame.GetComponent<MeshRenderer>().material;
        macron = GameObject.Find("Macron").transform;
        chicon = GameObject.Find("Chicon").transform;
        coloredTitle = GameObject.Find("ColoredTitle").GetComponent<Text>();
        canvasMat = klein.GetComponent<MeshRenderer>().material;
        colorSlider = GameObject.Find("ColorSlider").GetComponent<Slider>();
        stateSlider = GameObject.Find("StateSlider").GetComponent<Slider>();
        maxValueToggle = GameObject.Find("MaxValueToggle").GetComponent<Toggle>();
        sliderMoveDurationDisplay = GameObject.Find("SliderMoveDurationDisplay").GetComponent<Text>();
        text = GameObject.Find("Text").GetComponent<Text>();

        /*GOverride #037*/
        coloredWordDisplay = GetComponent<ColoredWordDisplay>();

        // Init
        frame.gameObject.SetActive(false);
        macron.gameObject.SetActive(false);
        chicon.gameObject.SetActive(false);
        maxValueToggle.gameObject.SetActive(false);
        stateSlider.gameObject.SetActive(false);
        saturation = 1f;
        buffer = null;
        bufferMesh = null;
        bufferMaterial = null;
        originalColoredTitleFontSize = coloredTitle.fontSize;
        maxColoredTitleSizeDiff = 5;
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
        lartText = GameObject.Find("Text_art").GetComponent<Text>();
        cestText = GameObject.Find("Text_cest").GetComponent<Text>();
        lavieText = GameObject.Find("Text_lavie").GetComponent<Text>();
        lartText.enabled = false;
        cestText.enabled = false;
        lavieText.enabled = false;



        Resources.UnloadAsset(xmlCorpus);
    }

    void Update()
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mouseWorldPosition.x > kleinMesh.bounds.max.x / 2f && mouseWorldPosition.x < kleinMesh.bounds.max.x &&
            mouseWorldPosition.y > kleinMesh.bounds.max.z / 2f && mouseWorldPosition.y < kleinMesh.bounds.max.z)
        {
            if (maxValueToggle.isOn)
            {
                saturation = 1f;

                if (!coloredWordDisplay.condition)
                {
                    if (colorSlider.value >= colorSlider.maxValue * 0.5f && colorSlider.value <= colorSlider.maxValue * 0.5f + 0.5f)
                    {
                        /*GOverride #037*/
                        coloredWordDisplay.condition = true;
                        //
                        //                buffer = Instantiate(bufferPrefab, new Vector3(kleinMesh.bounds.max.x - (kleinMesh.bounds.max.x / 4f), kleinMesh.bounds.min.z + (kleinMesh.bounds.max.z / 4f), -1f), Quaternion.identity);
                        //bufferMesh = buffer.GetComponent<MeshFilter>().mesh;
                        //bufferMaterial = buffer.GetComponent<MeshRenderer>().material;
                        //bufferMaterial.color = new Color(1f, 1f, 0f);
                    }
                }
            }
            else
            {
                saturation = 0.5f;
            }
        }

        if (chicon.gameObject.activeSelf)
        {
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            chicon.right = new Vector3(mouseWorldPosition.x - chicon.position.x, mouseWorldPosition.y - chicon.position.y, chicon.right.z).normalized;
            chicon.position += new Vector3(mouseWorldPosition.x - chicon.position.x, mouseWorldPosition.y - chicon.position.y, 0f) * Time.deltaTime;
        }

        if (macron.gameObject.activeSelf && coloredTitle.gameObject.activeSelf && playerRomain.getTimerWithMoving() > 25f)
        {
            chicon.gameObject.SetActive(true);
        }

        //if (Input.GetMouseButton(0))
        //{
        //    if (coloredWordDisplay.condition)
        //    {
        //        //if (mouseWorldPosition.x > coloredWordDisplay.title.position.x - (bufferMesh.bounds.extents.x)
        //        //    && mouseWorldPosition.x < buffer.position.x + (bufferMesh.bounds.extents.x)
        //        //    && mouseWorldPosition.y > buffer.position.y - (bufferMesh.bounds.extents.y)
        //        //    && mouseWorldPosition.y < buffer.position.y + (bufferMesh.bounds.extents.y))
        //        //{
        //        //    coloredWordDisplay.changeWord();
        //        //}
        //    }
        //}

        if (playerRomain.getTimerWithMoving() > 10f)
        {
            stateSlider.gameObject.SetActive(true);
            frameMaterial.mainTextureOffset = new Vector2(0f, 0f);
        }

        if (playerRomain.getTimerWithMoving() > 0)
        {
            text.text = texts[(int)(playerRomain.getTimerWithMoving() / 10f)];
            if (buffer != null)
            {
                text.color = bufferMaterial.color;
            }

            coloredTitle.fontSize += (int)(100 * Time.deltaTime);
            if (Mathf.Abs(coloredTitle.fontSize - originalColoredTitleFontSize) > maxColoredTitleSizeDiff)
            {
                coloredTitle.fontSize = originalColoredTitleFontSize;
            }

            maxColoredTitleSizeDiff = (int)playerRomain.getTimerWithMoving();
        }
        else
        {
            text.text = "";
        }
    }

    public void setColor()
    {
        if (state == States.NORMAL)
        {
            HSVColor hsvColor = new HSVColor(colorSlider.value, saturation, 1f);
            mr.sharedMaterial.color = hsvColor.ToColor();

            if (colorSlider.value > 0.25f && colorSlider.value < 0.5f && ((klein.eulerAngles.x > 300 && klein.eulerAngles.x < 360) || (klein.eulerAngles.x > 0 && klein.eulerAngles.x < 40)))
            {
                macron.gameObject.SetActive(true);
            }
        }
        else if (state == States.ROTATION)
        {
            klein.Rotate(Vector3.up, 20f * (colorSlider.value / colorSlider.maxValue));
            frame.Rotate(Vector3.up, 20f * (colorSlider.value / colorSlider.maxValue));
            hasReachedRotation = true;
        }
        else if (state == States.SCALE)
        {
            klein.localScale = new Vector3(colorSlider.value / colorSlider.maxValue, klein.localScale.y, colorSlider.value / colorSlider.maxValue);
            frame.localScale = new Vector3(colorSlider.value / colorSlider.maxValue, frame.localScale.y, colorSlider.value / colorSlider.maxValue);
        }

        if (Mathf.Approximately(colorSlider.value, 0.5f))
        {
            maxValueToggle.gameObject.SetActive(true);
        }
    }

    public void setMaxValue()
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

    public void setState()
    {
        if (stateSlider.value == 0)
        {
            state = States.NORMAL;
        }
        else if (stateSlider.value == 1)
        {
            state = States.ROTATION;
            frameMaterial.mainTextureOffset = new Vector2(0f, 0f);
        }
        else if (stateSlider.value == 2)
        {
            state = States.SCALE;
            frameMaterial.mainTextureOffset = new Vector2(0f, 0f);
        }
    }

    public void showFrame ()
    {
        frame.gameObject.SetActive(true);
    }

    public void offsetFrameTop()
    {
        frameMaterial.mainTextureOffset += new Vector2(0f, -0.05f);
    }

    public void offsetFrameBottom()
    {
        frameMaterial.mainTextureOffset += new Vector2(0f, 0.05f);
    }

    public void offsetFrameRight ()
    {
        frameMaterial.mainTextureOffset += new Vector2(0.05f, 0f);
    }

    public void offsetFrameLeft ()
    {
        frameMaterial.mainTextureOffset += new Vector2(-0.05f, 0f);
    }

    public void lart ()
    {
        if (chicon.gameObject.activeSelf && macron.gameObject.activeSelf)
        {
            lartText.enabled = true;
            cestText.enabled = true;
            lavieText.enabled = true;
        }
    }

    public States getCurrentState()
    {
        return state;
    }
}
