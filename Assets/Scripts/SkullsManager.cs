using UnityEngine;

public class SkullsManager : MonoBehaviour
{
    public int skullsDeliveredCount = 0;

    // Функция для обновления счетчика черепов
    public void UpdateSkullsDeliveredCount(int skullsInTrigger)
    {
        skullsDeliveredCount = skullsInTrigger;
        // Можно добавить здесь дополнительную логику при обновлении счетчика черепов
    }
}