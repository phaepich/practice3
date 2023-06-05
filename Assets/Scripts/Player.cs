using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float healPerSecond;
    public float health; 
    [SerializeField] private Canvas deadCanvas;
    [SerializeField] private Canvas finalCanvas;
    [SerializeField] private Transform vrCamera;

    public void TakeDamage(int damage)
    {
        if (health - damage <= 0)
        {
            TimerScript.StopTimer();
            Debug.Log("Умер");
            Time.timeScale = 0f;
            deadCanvas.enabled = true;
            deadCanvas.transform.position = vrCamera.position + vrCamera.forward * 0.5f;
            deadCanvas.transform.rotation = Quaternion.LookRotation(vrCamera.forward);
            finalCanvas.transform.position = vrCamera.position + vrCamera.forward * 0.5f;
            finalCanvas.transform.rotation = Quaternion.LookRotation(vrCamera.forward);
            return;
        }
        health -= damage;
    }

    public void SetNormalTime()
    {
        Time.timeScale = 1f;
    }
    
    private void Heal()
    {
        if (health >= maxHealth)
        {
            health = maxHealth;
            return;
        }
        health += Time.deltaTime * healPerSecond;
    }
    void Update()
    {
        Heal();
    }
}
