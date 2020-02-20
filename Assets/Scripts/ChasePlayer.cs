using Pathfinding;
using UnityEngine;


public class ChasePlayer : MonoBehaviour
{
    private AIPath aiPath;
    private AIDestinationSetter aiDestinationSetter;
    private State state;

    public GameObject empty;
    private GameObject thisEmpty;


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

