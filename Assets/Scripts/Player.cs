using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float healPerSecond;
    public float health;
    private ScreenFader screenFader;

    private void Start()
    {
        screenFader = GameObject.Find("Fader").GetComponent<ScreenFader>();
    }

    public void TakeDamage(int damage)
    {
        if (health - damage <= 0)
        {
            TimerScript.StopTimer();
            Debug.Log("Умер");
            screenFader.StartFade();
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
