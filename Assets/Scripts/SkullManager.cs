using UnityEngine;

public class SkullManager : MonoBehaviour
{
    public static SkullManager instance;
    public int skullCount = 2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Добавьте методы для управления количеством черепов
}