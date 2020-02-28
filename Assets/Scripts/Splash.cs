using System;
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
        Cyan,
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
            case Colors.Green:
                c = new Color(Random.Range(0.03f,0.15f),Random.Range(0.8f,1f),Random.Range(0.03f,0.15f));
                break;
            case Colors.Blue:
                c = new Color(rand,rand,Random.Range(0.5f,1.0f));
                break;
            case Colors.Orange:
                c = new Color(1f,Random.Range(0.0f,0.5f),Random.Range(0.0f,0.3f));
                break;
            case Colors.Cyan:
                c = new Color(0.0f,Random.Range(0.7f,1f),Random.Range(0.7f,1f));
                break;
            default:
                c = new Color(0,0,0);
                break;
        }
        return c;
    }
}
