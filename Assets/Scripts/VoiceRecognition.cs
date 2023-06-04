using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour
{
    [SerializeField] private string _phrase;
    private Dictionary<string, Action> _keywordActions = new Dictionary<string, Action>();
    public KeywordRecognizer _keywordRecognizer;
    private FinalMenu _finalMenu;
    [SerializeField] private Transform vrCamera;
    public bool isGameCompleted;
    void Start()
    {
        _finalMenu = FindObjectOfType<FinalMenu>();
        isGameCompleted = false;
        _keywordActions.Add(_phrase, CompleteGame);
        _keywordRecognizer = new KeywordRecognizer(_keywordActions.Keys.ToArray());
        _keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        _keywordRecognizer.Start();
    }

    void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword " + args.text);
        _keywordActions[args.text].Invoke();
    }
    void CompleteGame()
    {
        isGameCompleted = true;
        _finalMenu.transform.position = vrCamera.position + vrCamera.forward * 1;
        _finalMenu.transform.rotation = Quaternion.LookRotation(vrCamera.forward);
        _finalMenu.gameObject.SetActive(true);
        Debug.Log("Game completed");
    }
}
