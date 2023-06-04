using System.Timers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DeathScreenMenu : MonoBehaviour
{
    [SerializeField] private Canvas deathScreenMenuCanvas; // Ссылка на Canvas с окном смерти
    [SerializeField] private Transform vrCamera; // Ссылка на камеру в VR
    void Start()
    {
        deathScreenMenuCanvas.enabled = false; // Скрыть окно паузы при запуске
    }
    
    public static void OnDeath()
    {
        Time.timeScale = 0f;
        DeathScreenMenu deathScreenMenu = FindObjectOfType<DeathScreenMenu>();
        if (deathScreenMenu != null)
        {
            deathScreenMenu.deathScreenMenuCanvas.enabled = true;
            deathScreenMenu.deathScreenMenuCanvas.transform.position = deathScreenMenu.vrCamera.position + deathScreenMenu.vrCamera.forward * 1;
            deathScreenMenu.deathScreenMenuCanvas.transform.rotation = Quaternion.LookRotation(deathScreenMenu.vrCamera.forward);
        }
    }

    public static void LoadGame()
    {
        SaveLoadManager.LoadGame();
        DeathScreenMenu deathScreenMenu = FindObjectOfType<DeathScreenMenu>();
        if (deathScreenMenu != null)
        {
            deathScreenMenu.deathScreenMenuCanvas.enabled = false;
        }
    }

    public void Continue()
    {
        
    }
}