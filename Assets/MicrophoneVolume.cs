using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MicrophoneVolume : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    void Update()
    {
        text.text = "Громкость микрофона: (0-120) " + (int)(MicrophoneRecorder.volumeLevel * 1000f);
    }
}
