using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KleinWaterizer : MonoBehaviour {

    public Material waterDispMaterial;

    public bool condition = false;
    private bool activated = false;
    private bool initialized = false;
    private Light mainLight;

    public float lightXAngle = 150;
    public float maxLightIntensity = 1f;
    public float lightFadeInDuration = 1f;
    private float lightFadeInTimer = 0f;

	void Start ()
    {
        mainLight = GameObject.Find("WaterLight").GetComponent<Light>();
        mainLight.intensity = 0f;
        mainLight.transform.eulerAngles = new Vector3(lightXAngle, 0, 0);
    }
	
	void Update ()
    {
		if(condition && !initialized)
        {
            init();
        }

        if(activated)
        {
            //Animate light : rotation & intensity
            if(lightFadeInTimer < lightFadeInDuration)
            {
                lightFadeInTimer += Time.deltaTime;
                float ratio = lightFadeInTimer / lightFadeInDuration;
                mainLight.intensity = Mathf.Lerp(0, maxLightIntensity, ratio);
            }
            else
            {
                mainLight.intensity = maxLightIntensity;
            }

        }
	}

    private void init()
    {
        initialized = true;
        MeshRenderer kleinRenderer = GameObject.Find("Klein").GetComponent<MeshRenderer>();
        kleinRenderer.material = waterDispMaterial;

        activated = true;
    }

    public void activate()
    {
        condition = true;
        GetComponent<PlayerRomain>().btnActivated = true;
    }
}
