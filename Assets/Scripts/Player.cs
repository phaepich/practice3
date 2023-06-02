using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float healPerSecond;
    public float health;

    public void TakeDamage(int damage)
    {
        if (health - damage <= 0)
        {
            Debug.Log("Умер");
            return;
        }
        health -= damage;
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
