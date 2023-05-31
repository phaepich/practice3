using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFromMicrophone : MonoBehaviour
{
    public Vector3 minScale;

    public Vector3 macScale;
    
    public float threshold = 0.1f;

    public float loudnessSensibility = 100;
    void Update()
    {
        float loudness = MicrophoneRecorder.volumeLevel * loudnessSensibility;
        if (loudness < threshold) loudness = 0;
        transform.localScale = Vector3.Lerp(minScale, macScale, loudness);
    }
}
