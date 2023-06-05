using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour
{
    public string phrase;
    private Dictionary<string, Action> _keywordActions = new Dictionary<string, Action>();
    public KeywordRecognizer _keywordRecognizer;
    private bool isListening = false;
    [SerializeField] private FinalMenu _finalMenu;
    [SerializeField] private Transform vrCamera;
    [SerializeField] private PhraseText phraseText;
    public bool isGameCompleted;
    void Start()
    {
   
        _keywordActions.Add(phrase, CompleteGame);
        _keywordRecognizer = new KeywordRecognizer(_keywordActions.Keys.ToArray());
        _keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        _keywordRecognizer.Start();
        
    }

    void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword " + args.text);
        if (_keywordActions.ContainsKey(args.text))
        {
            _keywordActions[args.text].Invoke();
        }
    }
    public void ToggleListening(bool enable)
    {
        isListening = enable;


    }
    void CompleteGame()
    {
        isGameCompleted = true;
        Debug.Log("Game completed");
        _finalMenu.transform.position = vrCamera.position + vrCamera.forward * 1;
        _finalMenu.transform.rotation = Quaternion.LookRotation(vrCamera.forward);
        _finalMenu.gameObject.SetActive(true);
        
        Time.timeScale = 0f;
    }
    public void OnDisable()
    {
        if (_keywordRecognizer != null && _keywordRecognizer.IsRunning)
        {
            _keywordRecognizer.Stop();
        }
    }
}
