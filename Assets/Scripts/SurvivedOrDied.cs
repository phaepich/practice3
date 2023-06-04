using System;
using TMPro;
using UnityEngine;

public class SurvivedOrDied : MonoBehaviour
{
    private TMP_Text _text;
    private VoiceRecognition _voiceRecognition;

    private void Start()
    {
        _voiceRecognition = FindObjectOfType<VoiceRecognition>();
        if (_voiceRecognition.isGameCompleted)
        {
            _text.text = "ВЫЖИЛ";
        }
        else
        {
            _text.text = "ПОМЕР";
        }
    }
}
