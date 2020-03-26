using Pathfinding;
using UnityEngine;
public class ChasePlayer : MonoBehaviour
{
    private AIPath _aiPath;
    private AIDestinationSetter _aiDestinationSetter;
    private State _state;

    public GameObject empty;
    private GameObject _thisEmpty;


    void Start()
    {
        _aiPath = gameObject.GetComponent<AIPath>();
        _aiDestinationSetter = gameObject.GetComponent<AIDestinationSetter>();
        _thisEmpty = Instantiate(empty, transform.position, Quaternion.identity);
    }

    public enum State 
    {
        Idle,
        Chase,
        GoBack,
    }

    public void Chase(Transform other, State newState)
    {
        _aiDestinationSetter.target = other;
    }

    public void InvokeRetreat(float time)
    {
        Invoke(nameof(Retreat),time);
    }

    private void Retreat()
    {
        _aiDestinationSetter.target = _thisEmpty.transform;
    } 
}

