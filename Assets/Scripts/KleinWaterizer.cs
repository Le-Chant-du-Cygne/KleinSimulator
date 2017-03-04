using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KleinWaterizer : MonoBehaviour {

    public Material waterDispMaterial;


    private bool condition = false;
    private bool initialized = false;
    private Light mainLight;

	void Start ()
    {
        mainLight = GameObject.Find("WaterLight").GetComponent<Light>();
        mainLight.intensity = 0f;
        mainLight.transform.forward = Vector3.forward;
    }
	
	void Update () {
		if(condition && !initialized)
        {
            init();
        }
	}

    private void init()
    {
        initialized = true;
        MeshRenderer kleinRenderer = GameObject.Find("Klein").GetComponent<MeshRenderer>();
        kleinRenderer.material = waterDispMaterial;
    }

}
