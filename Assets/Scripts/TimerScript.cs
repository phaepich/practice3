using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public TMP_Text timerText;
    private static bool isRunning;
    private static float startTime;

    void Start()
    {
        isRunning = true;
        startTime = Time.time;
    }

    void Update()
    {
        if (isRunning)
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString("00");
            string seconds = (t % 60).ToString("00");

            timerText.text = "ВРЕМЯ ИГРЫ: " + minutes + ":" + seconds;
        }
    }

    public static void StopTimer()
    {
        isRunning = false;
    }

    public static void StartTimer()
    {
        isRunning = true;
    }
}