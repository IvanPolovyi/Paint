using System;
using System.Collections;
using UnityEngine;
public class Foot : MonoBehaviour
{
    public bool grounded;

    public LayerMask whatIsGround;
    public float speed = 1;
    
    private Rigidbody2D _rb;

    
    private Vector2 _destination;
    private bool _trigger;
    private bool _readyToMove;
    private bool _readyToPutDown;
    private bool _rayCasting = true;
    private bool _readyToLift;
    private bool _stepInProgress;
    private Vector2 _initialPos;
    
    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        
        if (_trigger && grounded && !_stepInProgress)
        {
            _stepInProgress = true;
            _trigger = false;
            _readyToLift = true;
            grounded = false;
        }

        if (_readyToLift)
        {
            _readyToLift = false;
            _rayCasting = false;
            StartCoroutine(Lift(0.5f));
        }

        if (_readyToMove)
        {
            _readyToMove = false;
            StartCoroutine(Move(-5));
        }

        if (_readyToPutDown)
        {
            _readyToPutDown = false;
            _rayCasting = true;
            StartCoroutine(PutDown(0.5f));
        }
        
    }

    private int _coroutineDebug = 0;
    void FixedUpdate()
    {
        if (!_rayCasting) return;

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.localPosition, Vector2.down, 0.2f, whatIsGround);
        grounded = hitInfo.collider != null;
    }
    
    private IEnumerator Lift(float liftSpeed)
    {
        _coroutineDebug++;
        for (int i = 0; i < 10; i++)
        {
            _rb.position += Vector2.up * liftSpeed;
            yield return new WaitForEndOfFrame();
        }
        _readyToMove = true;
    }
    private IEnumerator Move(float dir)
    {
        _coroutineDebug++;
        for (int i = 0; i < 10; i++)
        {
            _rb.position += new Vector2(speed * dir, 0) * 0.1f;
            yield return new WaitForEndOfFrame();
        }
       
        _readyToPutDown = true;
    }
    private IEnumerator PutDown(float liftSpeed)
    {
        _coroutineDebug++;
        for (int i = 0; i < 10; i++)
        {
            _rb.position += Vector2.down*liftSpeed;
            if (grounded)
            {
                _stepInProgress = false;
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
        
    }


    public void SetStep(Vector2 destination, bool trigger)
    {
        if (_readyToMove) return;
        
        _destination = destination;
        _trigger = trigger;
    }
}

