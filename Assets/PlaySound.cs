using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlaySound : MonoBehaviour
{
    private AudioSource sourse;

    private SphereCollider soundTrigger;

    private void Awake()
    {
        sourse = GetComponent<AudioSource>();
        soundTrigger = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (transform.position != default)
        {
            sourse.Play();
        }
    }
}
