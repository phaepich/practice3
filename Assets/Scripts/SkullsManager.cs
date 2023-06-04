using System;
using UnityEngine;

public class SkullsManager : MonoBehaviour
{
    public int skullsDeliveredCount = 0;
    private VoiceRecognition _voiceRecognition;

    private void Start()
    {
        _voiceRecognition = FindObjectOfType<VoiceRecognition>();
    }

    public void UpdateSkullsDeliveredCount(int skullsInTrigger)
    {
        skullsDeliveredCount = skullsInTrigger;
        if (skullsDeliveredCount == 4)
        {
            _voiceRecognition.ToggleListening(true);
        }
    }
}