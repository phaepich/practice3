using System;
using TMPro;
using UnityEngine;

public class SurvivedOrDied : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private VoiceRecognition _voiceRecognition;

    private void Start()
    {
        _voiceRecognition = FindObjectOfType<VoiceRecognition>();
        if (_voiceRecognition.isGameCompleted)
        {
            text.text = "ВЫЖИЛ";
        }
        else
        {
            text.text = "ПОМЕР";
        }
    }

    private void Update()
    {
        
    }
}
