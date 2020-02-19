using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    public bool colorIsSet;
    public GameObject[] splashes;
    public Color color;

    void Start()
    {
        int rand = Random.Range(0, splashes.Length);
        Instantiate(splashes[rand], transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(-180,180)));


    }
}
