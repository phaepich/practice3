using System;
using UnityEngine;

public class SkullsManager : MonoBehaviour
{
    public int skullsDeliveredCount = 0;
    public VoiceRecognition _voiceRecognition;
    public PhraseText phraseText;

    public void UpdateSkullsDeliveredCount(int skullsInTrigger)
    {
        skullsDeliveredCount = skullsInTrigger;
        if (skullsDeliveredCount == 4)
        {
            _voiceRecognition.ToggleListening(true);
            phraseText.gameObject.SetActive(true);
        }
    }
}