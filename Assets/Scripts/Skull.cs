using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    private SkullsManager skullsManager;

    private void Start()
    {
        skullsManager = GameObject.FindObjectOfType<SkullsManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            // Передаем череп в триггер для обновления счетчика
            other.GetComponent<SkullTriggerHandler>().UpdateSkullsCount(1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            // Передаем череп в триггер для обновления счетчика
            other.GetComponent<SkullTriggerHandler>().UpdateSkullsCount(-1);
        }
    }
}

