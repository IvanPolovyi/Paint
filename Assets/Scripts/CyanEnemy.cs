using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CyanEnemy : MonoBehaviour
{
    public Animator animator;
    private Transform target;
    public LayerMask whatIsPlayer;
    public int health;
    private float _timeSinceLastDash;
    public float dashRate = 1f;
    private AIPath path;
    private AIDestinationSetter setter;
    private ChasePlayer chasePlayer;
    public GameObject deathEffect;
    private RipplePostProcessor camRipple;
    public float dashSpeed = 10;
    private Vector3 direction;
    public int damage = 1;

    private bool dash;
    private Vector3 curDir = Vector3.zero;
    public GameObject dushSplash;
    void Start()
    {
        path = gameObject.GetComponentInParent<AIPath>();
        setter = gameObject.GetComponentInParent<AIDestinationSetter>();
        chasePlayer  = gameObject.GetComponentInParent<ChasePlayer>();
        setter.target = chasePlayer.empty.transform;
        CDTime = dashRate * 0.8f;
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, ( target.position - transform.position), 200f, whatIsPlayer);
        

        direction = (target.transform.position - transform.position).normalized;
        if (dash==false)
            curDir = direction;
        
        Debug.DrawRay(transform.position,curDir , Color.red, 0.1f);
        if (hit2D.collider != null)
        {
            if (setter.target == this.target
                && !hit2D.collider.gameObject.CompareTag("Player"))
            {
                path.canSearch = true;
            }

            _timeSinceLastDash += Time.deltaTime;
            thisCDTime += Time.deltaTime;
            if (_timeSinceLastDash >= dashRate
                && hit2D.collider.gameObject.CompareTag("Player")
            )
            {
                _timeSinceLastDash = 0;
                dash = true;
                path.canMove = false;
            }

            if (dash)
            {
                path.canSearch = false;
                transform.parent.transform.position += curDir * dashSpeed * Time.deltaTime;
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
        if(!dash) return;
        
        if(other.gameObject.CompareTag("Player"))
        {
            dash = false;
            FinishDash();
            other.gameObject.GetComponent<PlayerMove>().TakeDamage(damage);
        }
        else if(other.gameObject.CompareTag("Environment")
        ||other.gameObject.CompareTag("Enemy"))
        {
            dash = false;
            FinishDash();
        }
    }

    private float CDTime;
    private float thisCDTime;
    private void FinishDash()
    {
        path.canMove = true;
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
