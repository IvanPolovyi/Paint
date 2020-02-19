using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemColor : MonoBehaviour
{
    private ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        var main = ps.main;
        float rand = Random.Range(0.0f, 0.15f);
        main.startColor = new Color(rand,rand,Random.Range(0.5f,1.0f));
    }

}
