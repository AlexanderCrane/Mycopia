using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScaler : MonoBehaviour
{
    private ParticleSystem ps;
    public float sliderValue = 1.0F;
    public float parentSliderValue = 1.0F;
    public ParticleSystemScalingMode scaleMode;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        ps.transform.localScale = new Vector3(sliderValue, sliderValue, sliderValue);
        if (ps.transform.parent != null)
            ps.transform.parent.localScale = new Vector3(parentSliderValue, parentSliderValue, parentSliderValue);

        var main = ps.main;
        main.scalingMode = scaleMode;
    }
}
