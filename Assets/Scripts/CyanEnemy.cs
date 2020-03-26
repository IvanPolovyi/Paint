using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CyanEnemy : MonoBehaviour, IDamagable
{
    public Animator animator;
    public LayerMask whatIsPlayer;
    public int health;
    public float dashRate = 1f;
    public float dashSpeed = 10;
    public int damage = 1;
    public GameObject dushSplash;
    public GameObject deathEffect;

    private AIPath _path;
    private AIDestinationSetter _setter;
    private ChasePlayer _chasePlayer;
    private RipplePostProcessor _camRipple;
    private Vector3 _direction;
    private Transform _target;
    private float _timeSinceLastDash;
    private bool _dash;
    private Vector3 _curDir = Vector3.zero;
    void Start()
    {
        _path = gameObject.GetComponentInParent<AIPath>();
        _setter = gameObject.GetComponentInParent<AIDestinationSetter>();
        _chasePlayer  = gameObject.GetComponentInParent<ChasePlayer>();
        _setter.target = _chasePlayer.empty.transform;
        CDTime = dashRate * 0.8f;
        _target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, ( _target.position - transform.position), 200f, whatIsPlayer);
        

        _direction = (_target.transform.position - transform.position).normalized;
        if (_dash==false)
            _curDir = _direction;
        
        Debug.DrawRay(transform.position,_curDir , Color.red, 0.1f);
        if (hit2D.collider != null)
        {
            if (_setter.target == _target
                && !hit2D.collider.gameObject.CompareTag("Player"))
            {
                _path.canSearch = true;
            }

            _timeSinceLastDash += Time.deltaTime;
            thisCDTime += Time.deltaTime;
            if (_timeSinceLastDash >= dashRate
                && hit2D.collider.gameObject.CompareTag("Player")
            )
            {
                _timeSinceLastDash = 0;
                _dash = true;
                _path.canMove = false;
            }

            if (_dash)
            {
                _path.canSearch = false;
                transform.parent.transform.position += _curDir * dashSpeed * Time.deltaTime;
            }
        }


        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(-180,180)));
            Destroy(transform.parent.gameObject);
            Camera.main.GetComponent<RipplePostProcessor>().RippleEffect(gameObject.transform.position);
        }
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!_dash) return;
        
        if(other.gameObject.CompareTag("Player"))
        {
            _dash = false;
            FinishDash();
            other.gameObject.GetComponent<PlayerStatus>().TakeDamage(damage);
        }
        else if(other.gameObject.CompareTag("Environment")
        ||other.gameObject.CompareTag("Enemy"))
        {
            _dash = false;
            FinishDash();
        }
    }

    private float CDTime;
    private float thisCDTime;
    private void FinishDash()
    {
        _path.canMove = true;
        if (thisCDTime >= CDTime)
        {
            Instantiate(dushSplash, transform.position, Quaternion.identity);
            TakeDamage(damage);
            thisCDTime = 0;
        }
    }
    

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("TakeDamage");
        health -= damage;
    }
}
