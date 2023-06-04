using System;
using UnityEngine;

public class SkullsManager : MonoBehaviour
{
    public int skullsDeliveredCount = 0;
    private VoiceRecognition _voiceRecognition;

    private void Start()
    {
        _voiceRecognition = FindObjectOfType<VoiceRecognition>();
        _voiceRecognition.enabled = false;
        _voiceRecognition._keywordRecognizer.Stop();
    }

    public void UpdateSkullsDeliveredCount(int skullsInTrigger)
    {
        skullsDeliveredCount = skullsInTrigger;
        if (skullsDeliveredCount == 4)
        {
            _voiceRecognition.enabled = true;
            _voiceRecognition._keywordRecognizer.Start();
        }
    }
}