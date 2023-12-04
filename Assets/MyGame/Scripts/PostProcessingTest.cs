using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class PostProcessingTest : MonoBehaviour
{
    public Volume postProcessingVolume;
    private float value = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnDamage();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(postProcessingVolume.profile.TryGet(out Bloom bloom))
            {
                bloom.intensity.value = 10;
            }
        }
    }
    private void OnDamage()
    {
        if(postProcessingVolume.profile.TryGet(out Vignette vignette))
        {
            value += 0.2f;
            value = Mathf.Clamp(value, 0, 0.6f);
            vignette.intensity.value = value;
        }
    }
}
