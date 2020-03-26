using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintVial : MonoBehaviour
{
    public AudioClip sound;
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
