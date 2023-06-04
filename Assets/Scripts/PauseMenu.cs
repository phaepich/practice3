using System.Timers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenuCanvas; // Ссылка на Canvas с окном паузы
    [SerializeField] private Canvas settingsMenuCanvas;
    [SerializeField] private Transform vrCamera; // Ссылка на камеру в VR
    [SerializeField] private float spawnDistance = 2f;
    [SerializeField] private InputActionProperty showButton;
    private bool isPaused = false; // Флаг для отслеживания состояния паузы

    void Start()
    {
        pauseMenuCanvas.enabled = false; // Скрыть окно паузы при запуске
    }

    void Update()
    {
        // Проверяем нажатие на клавишу Escape на клавиатуре или кнопку "Меню" на левом контроллере
        if (showButton.action.WasPressedThisFrame())
        {
            if (isPaused)
            {
                ResumeGame(); // Если игра уже на паузе, продолжаем игру
            }
            else
            {
                PauseGame(); // Если игра не на паузе, ставим игру на паузу
            }
        }
    }

    void PauseGame()
    {
        isPaused = true;
        TimerScript.StopTimer();
        Time.timeScale = 0f;
        pauseMenuCanvas.enabled = true;

        pauseMenuCanvas.transform.position = vrCamera.position + vrCamera.forward * spawnDistance;
        pauseMenuCanvas.transform.rotation = Quaternion.LookRotation(vrCamera.forward);
        settingsMenuCanvas.transform.position = vrCamera.position + vrCamera.forward * spawnDistance;
        settingsMenuCanvas.transform.rotation = Quaternion.LookRotation(vrCamera.forward);
    }

    public void ResumeGame()
    {
        isPaused = false;
        TimerScript.StartTimer();
        Time.timeScale = 1f; // Восстанавливаем скорость времени
        pauseMenuCanvas.enabled = false; // Скрываем окно паузы
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
        Debug.Log("Loading menu");
    }
}