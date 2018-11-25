using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScript : MonoBehaviour {
    public Light attachedLight;
    public FoveInterfaceBase foveInterface;

    private Collider instrument_face;
    private Material light_material;
    private bool light_attached = false;

	// Use this for initialization
	void Start () {
        instrument_face = GetComponent<Collider>();

        if (attachedLight == null)
            attachedLight = transform.GetComponentInChildren<Light>(); // get the light component

        if (attachedLight)
        {
            light_attached = true;
            attachedLight.enabled = false; // light is not on when initializing
        }
        light_material = gameObject.GetComponent<Renderer>().material; // grab the material for possible lighting changes

        if (light_material == null)
            gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (foveInterface.Gazecast(instrument_face))
        {
            light_material.EnableKeyword("_EMISSION");

            if (light_attached)
            {
                light_material.SetColor("_EmissionColor", attachedLight.color);
                attachedLight.enabled = true;
                DynamicGI.SetEmissive(GetComponent<Renderer>(), attachedLight.color);
            }
        }
        else
        {
            light_material.DisableKeyword("_EMISSION");
            gameObject.GetComponent<Renderer>().material.color = Color.white;

            if (light_attached)
            {
                attachedLight.enabled = false;
                DynamicGI.SetEmissive(GetComponent<Renderer>(), Color.black);
            }

        }
	}
}