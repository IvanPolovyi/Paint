using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.UNetWeaver;
using UnityEngine;

public class ChasePlaer : MonoBehaviour
{
    private AIPath aiPath;
    private AIDestinationSetter aiDestinationSetter;
    private State state;

    public GameObject empty;
    public GameObject thisEmpty;

    void Start()
    {
        aiPath = gameObject.GetComponent<AIPath>();
        aiDestinationSetter = gameObject.GetComponent<AIDestinationSetter>();


        thisEmpty = Instantiate(empty, transform.position, Quaternion.identity);
    }

    public enum State 
    {
        Idle,
        Chase,
        GoBack,
    }
    
    void Update()
    {


    }

    public void Chase(Transform other, State newState)
    {
        aiDestinationSetter.target = other;
    }

    public void InvokeRetreat(float time)
    {
       Invoke(nameof(Retreat),time);
    }

    private void Retreat()
    {
        aiDestinationSetter.target = thisEmpty.transform;
    } 
}
