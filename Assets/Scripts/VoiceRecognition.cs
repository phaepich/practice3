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
    private bool isListening = false;
    [SerializeField] private FinalMenu _finalMenu;
    [SerializeField] private Transform vrCamera;
    public bool isGameCompleted;
    void Start()
    {
        if (isListening)
        {
            _keywordActions.Add(_phrase, CompleteGame);
            _keywordRecognizer = new KeywordRecognizer(_keywordActions.Keys.ToArray());
            _keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
            _keywordRecognizer.Start();
        }
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

        if (isListening)
        {
            _keywordActions.Add(_phrase, CompleteGame);
            _keywordRecognizer = new KeywordRecognizer(_keywordActions.Keys.ToArray());
            _keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
            _keywordRecognizer.Start();
        }
        else
        {
            _keywordRecognizer.Stop();
            _keywordRecognizer.Dispose();
            _keywordActions.Clear();
        }
    }
    void CompleteGame()
    {
        isGameCompleted = true;
        Debug.Log("Game completed");
        _finalMenu.transform.position = vrCamera.position + vrCamera.forward * 1;
        _finalMenu.transform.rotation = Quaternion.LookRotation(vrCamera.forward);
        Time.timeScale = 0f;
        _finalMenu.gameObject.SetActive(true);
    }
    public void OnDisable()
    {
        if (_keywordRecognizer != null && _keywordRecognizer.IsRunning)
        {
            _keywordRecognizer.Stop();
        }
    }
}
