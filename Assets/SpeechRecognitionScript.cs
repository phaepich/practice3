using UnityEngine;
using Vosk;
using System;

public class SpeechRecognitionScript : MonoBehaviour
{
    private Model model;
    private VoskRecognizer recognizer;
    public string keyword; // Ключевая фраза, вводимая в инспекторе

    void Start()
    {
        model = new Model("path_to_model"); // Замените "path_to_model" на путь к модели распознавания речи Vosk. Может потребоваться указать полный путь, например, "C:/path_to_model".
        recognizer = new VoskRecognizer(model);
        recognizer.SetMaxAlternatives(0);
        recognizer.SetKeyword(keyword); // Установите ключевую фразу для распознавания
        recognizer.PartialResult += OnPartialResult;
        recognizer.FinalResult += OnResult;
    }

    void OnPartialResult(string hypothesis)
    {
        Debug.Log("Partial result: " + hypothesis);
    }

    void OnResult(string hypothesis)
    {
        Debug.Log("Recognized: " + hypothesis);
        if (hypothesis.ToLower().Equals(keyword.ToLower()))
        {
            Debug.Log("Игра пройдена");
        }
    }

    void Update()
    {
        // Запустите распознавание речи во время всего игрового процесса
        // Можете изменить условия и методы запуска распознавания в соответствии с вашей игрой
        if (Input.GetKeyDown(KeyCode.Space))
        {
            recognizer.Start();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            recognizer.Stop();
        }
    }

    void OnDestroy()
    {
        recognizer.Dispose();
        model.Dispose();
    }
}