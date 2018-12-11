using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transparent : MonoBehaviour
{
    public FoveInterfaceBase foveInterface;

    private Material mat;
    private Collider surface;
    // Use this for initialization
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.color = new Color(1.0f, 1.0f, 1.0f, 0.7f);

        surface = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (foveInterface.Gazecast(surface))
        {
            mat.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        {
            mat.color = new Color(1.0f, 1.0f, 1.0f, 0.7f);
        }
    }
}