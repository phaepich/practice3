using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullTriggerHandler : MonoBehaviour
{
    private int skullsInTrigger = 0;

    public SkullsManager skullsManager;

    private void Start()
    {
        skullsManager = FindObjectOfType<SkullsManager>();
    }

    public void UpdateSkullsCount(int value)
    {
        skullsInTrigger += value;
        skullsManager.UpdateSkullsDeliveredCount(skullsInTrigger);
    }
}
