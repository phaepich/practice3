using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MicrophoneVolume : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    void Update()
    {
        text.text = "Громкость микрофона (%): " + (int)(MicrophoneRecorder.volumeLevel * 100f / 0.12f) + "%";
    }
}
