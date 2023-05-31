using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneRecorder : MonoBehaviour
{
    public TMP_Dropdown microphoneDropdown;
    public static float volumeLevel;

    private AudioSource audioSource;
    private string selectedMicrophone;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Получение списка доступных микрофонов
        string[] microphoneDevices = Microphone.devices;
        
        // Заполнение Dropdown и установка обработчика события выбора микрофона
        microphoneDropdown.ClearOptions();
        microphoneDropdown.AddOptions(new List<string>(microphoneDevices));
        selectedMicrophone = Microphone.devices[0];
        microphoneDropdown.onValueChanged.AddListener(OnMicrophoneDropdownValueChanged);

        // Установка начального выбранного микрофона
        if (microphoneDevices.Length > 0)
        {
            microphoneDropdown.value = 0;
            OnMicrophoneDropdownValueChanged(0);
        }
    }

    void OnMicrophoneDropdownValueChanged(int index)
    {
        // Остановка записи звука с предыдущего микрофона
        Microphone.End(null);
        
        // Начало записи звука с выбранного микрофона
        selectedMicrophone = Microphone.devices[index];
        audioSource.clip = Microphone.Start(selectedMicrophone, true, 1, AudioSettings.outputSampleRate);
        audioSource.loop = true;

        // Запуск воспроизведения записанного звука
        audioSource.Play();
    }

    void Update()
    {
        // Получение уровня громкости звука
        float[] samples = new float[64];
        int microphonePosition = Microphone.GetPosition(selectedMicrophone) - 64;
        if (microphonePosition < 0)
        {
            return;
        }
        audioSource.clip.GetData(samples, microphonePosition);

        float sum = 0f;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += Mathf.Abs(samples[i]);
        }

        volumeLevel = sum / samples.Length;
    }
}