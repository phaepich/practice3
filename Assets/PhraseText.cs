using UnityEngine;
using TMPro;

public class PhraseText : MonoBehaviour
{
    [SerializeField] private VoiceRecognition voiceRecognition;
    [SerializeField] private TMP_Text text;
    void Start()
    {
        text.text = "Say: \n" + voiceRecognition.phrase;
    }
}
