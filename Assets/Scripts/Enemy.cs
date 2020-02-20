//using System;
using System.Collections;
using System.Collections.Generic;
using PreFabs.Enemies.Spider;
using UnityEngine;    

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public float moveVel;
    public int health;
    public float playerFindDistance;
    public Rigidbody2D rb;
    private Vector2 movement;
    public Leg leg;
    public LayerMask whatIsPlayer;
    
    private Transform player;
    private float distanceToPlayer;
    public GameObject deathEffect;
    private RipplePostProcessor camRipple;
    public float stepRate = 0.6f;
    private float _timeSinceLastStep = 0;
    
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().transform;

       
    }

    private void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        movement = direction;

        if (health <= 0)
        {
            
            Instantiate(deathEffect, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(-180,180)));
            moveVel = 0;
            Destroy(transform.parent.gameObject);
            Camera.main.GetComponent<RipplePostProcessor>().RippleEffect(gameObject.transform.position);
        }
        
        distanceToPlayer = Mathf.Sqrt((player.position.x - transform.position.x) *
                                      (player.position.x - transform.position.x) +
                                      (player.position.y - transform.position.y) *
                                      (player.position.y - transform.position.y));
        if (distanceToPlayer <= playerFindDistance)
        {
            moveCharacter(movement);
        }
        
        _timeSinceLastStep += Time.deltaTime;

        if (_timeSinceLastStep >= stepRate)
        {
            _timeSinceLastStep = 0;
            Step();
        }
    }

    void Step()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, ( player.position - transform.position), 40f, whatIsPlayer);
            
        if (distanceToPlayer <= 23 
            && hit2D.collider != null 
            && hit2D.collider.CompareTag("Player"))
        {
            leg.Step(player.position);
        }
        else 
            leg.Step(transform.position-new Vector3(4,4,0));
        
        
    }

    void moveCharacter(Vector3 direction)
    {
        {
          //  rb.MovePosition((Vector2) transform.position + (moveVel * Time.deltaTime * direction));
            transform.localPosition += moveVel * Time.deltaTime * direction;
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }

    public float getDistanceToPlayer()
    {
        return distanceToPlayer;
    }
}


