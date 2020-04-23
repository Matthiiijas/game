using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_flicker : MonoBehaviour
{
    Light light;

    public float minIntensity, maxIntensity, flickerSmoothness;

    void Start() {
        light = GetComponent<Light>();
    }

    void Update() {
        light.intensity = Mathf.Lerp(light.intensity, Random.Range(minIntensity, maxIntensity), flickerSmoothness);
    }
}
