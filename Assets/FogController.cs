using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour {
    public float fog_Density;


    // Update is called once per frame
    void Update()
    {
        RenderSettings.fogDensity = fog_Density;
    }
}
