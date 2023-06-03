using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectedSculls : MonoBehaviour
{
    public string snapZoneTag = "SnapZone";
    public int visible = 0;
    public int counter = 0;
    private void Update()
    {
        
        int visibleSnapZones = CountVisibleSnapZones();
    }

    private int CountVisibleSnapZones()
    {
        

        GameObject[] snapZones = GameObject.FindGameObjectsWithTag(snapZoneTag);
        foreach (GameObject snapZone in snapZones)
        {
            if (IsVisible(snapZone))
            {
                visible++;
                
            }
            break;
        }

        return visible;
    }

    private void OnTriggerEnter(Collider Scull)
    {
        if (visible >= 1 && Scull.gameObject.tag == "SnapZone")
        {
            counter++;
        }
    }

    private bool IsVisible(GameObject obj)
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(obj.transform.position);
        bool isVisible = viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1 && viewPos.z > 0;
        return isVisible;
    }
}