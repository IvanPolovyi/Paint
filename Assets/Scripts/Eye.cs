﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    private Vector2 movement;
    void Update()
    {
        
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.position = (Vector2) difference;
    }
}
