using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PauseMenu2 : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuCanvas; // Ссылка на Canvas с окном паузы
    [SerializeField] private Transform vrCamera; // Ссылка на камеру в VR
    [SerializeField] private float spawnDistance = 2f;
    [SerializeField] private InputActionProperty showButton;
    private bool isPaused = false; // Флаг для отслеживания состояния паузы

    void Start()
    {
        pauseMenuCanvas.SetActive(false); // Скрыть окно паузы при запуске
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
        Time.timeScale = 0f; // Останавливаем время
        pauseMenuCanvas.SetActive(true); // Показываем окно паузы

        // Позиционируем окно паузы перед глазами игрока
        pauseMenuCanvas.transform.position = vrCamera.position + vrCamera.forward * spawnDistance;
        pauseMenuCanvas.transform.rotation = Quaternion.LookRotation(vrCamera.forward);
    }

    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Восстанавливаем скорость времени
        pauseMenuCanvas.SetActive(false); // Скрываем окно паузы
    }
}