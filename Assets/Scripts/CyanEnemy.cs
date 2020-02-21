using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CyanEnemy : MonoBehaviour
{

    public Transform target;
    public LayerMask whatIsPlayer;
    public int health;
    private float _timeSinceLastDash;
    public float dashRate = 1f;
    private AIPath path;
    private AIDestinationSetter setter;
    private ChasePlayer chasePlayer;
    public GameObject deathEffect;
    private RipplePostProcessor camRipple;
    public float dashSpeed = 100f;
    private Vector2 direction;
    private bool collided=true;
    private bool dash;
    void Start()
    {
        path = gameObject.GetComponentInParent<AIPath>();
        setter = gameObject.GetComponentInParent<AIDestinationSetter>();
        chasePlayer  = gameObject.GetComponentInParent<ChasePlayer>();
        setter.target = chasePlayer.empty.transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, ( target.position - transform.position), 200f, whatIsPlayer);
        

        direction = (target.transform.position - transform.position).normalized;
        
        Vector2 curDir = Vector2.zero;
        if (!dash)
            curDir = direction;
        Debug.DrawRay(transform.position,curDir , Color.red, 0.1f);
        if (hit2D.collider != null)
        {
            if(setter.target == this.target
               && !hit2D.collider.gameObject.CompareTag("Player"))
            {
                path.canSearch = true;
            }
            
            _timeSinceLastDash += Time.deltaTime;
            if (_timeSinceLastDash >= dashRate
                && hit2D.collider.gameObject.CompareTag("Player")
                && collided)
            {
                path.canSearch = false;
                collided = false;
                _timeSinceLastDash = 0;
                dash = true;
            }

            if (dash)
            {
                transform.position = curDir * dashSpeed * Time.deltaTime;
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
        dash = false;
    }
}
