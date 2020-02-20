﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Splash : MonoBehaviour
{
    public bool colorIsSet;
    public GameObject[] splashes;
    public Colors color;

    public enum Colors
    {
        Red,
        Orange,
        Blue,
        Green
    }
    void Start()
    {
        int rand = Random.Range(0, splashes.Length);
        var splash = Instantiate(splashes[rand], transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(-180,180))) as GameObject;
        splash.GetComponent<SpriteRenderer>().color = SetColor();
    }

    Color SetColor()
    {
        Color c;
        float rand = Random.Range(0.0f, 0.15f);
        switch (color)
        {
            case Colors.Blue:
                c = new Color(rand,rand,Random.Range(0.5f,1.0f));
                break;
            case Colors.Orange:
                c = new Color(1f,Random.Range(0.0f,0.5f),Random.Range(0.0f,0.3f));
                break;
            default:
                c = new Color(0,0,0);
                break;
        }
        return c;
    }
}
