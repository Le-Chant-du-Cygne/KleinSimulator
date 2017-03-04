using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundGeneration : MonoBehaviour {

    private System.Random RandomNumber = new System.Random();
    public float offset = 0;
    private float gain;

    public AudioMixer mixer;
    private Slider colorSlider;


    void Start () {

        gain = 0.2f;

        colorSlider = GameObject.Find("ColorSlider").GetComponent<Slider>();
        mixer.SetFloat("EQFreq", Mathf.Lerp(220, 22000, colorSlider.value));

    }

    void Update () {


    }


    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = offset - 1.0f + (float)RandomNumber.NextDouble() * 2.0f;
            data[i] = data[i] * gain;
        }
    }


  public void SetSoundEQFreq()
    {
        
        mixer.SetFloat("EQFreq", Mathf.Lerp(220, 22000, colorSlider.value));
        
    }
}
