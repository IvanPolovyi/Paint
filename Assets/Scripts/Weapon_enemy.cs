using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Weapon_enemy : MonoBehaviour
{
    public float offset;
    public GameObject projectile;
    public Transform shotPoint;
    private Transform target;
    public LayerMask whatIsPlayer;
    private float timeBtwShots;
    private float rotZ;
    private float _timeSinceLastShoot;
    public float shootRate = 2f;
    private AIPath path;
    private AIDestinationSetter setter;
    private ChasePlayer chasePlayer;


    private void Start()
    {
        path = gameObject.GetComponentInParent<AIPath>();
        setter = gameObject.GetComponentInParent<AIDestinationSetter>();
        chasePlayer  = gameObject.GetComponentInParent<ChasePlayer>();
        setter.target = chasePlayer.empty.transform;
        target = GameObject.FindWithTag("Player").transform;


    }

    private void Update()
    {

        
        Vector3 difference = target.position - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        _timeSinceLastShoot += Time.deltaTime;
        
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, ( target.position - transform.position), 200f, whatIsPlayer);
        if (hit2D.collider != null)
        {
            if(setter.target == this.target
               && !hit2D.collider.gameObject.CompareTag("Player"))
            {
                path.canSearch = true;
            }
        
            if (_timeSinceLastShoot >= shootRate
                && hit2D.collider.gameObject.CompareTag("Player"))
            {
                path.canSearch = false;
                _timeSinceLastShoot = 0;
                Shoot();
            }
        }


        if (setter.target.CompareTag("Empty"))
        {
            path.canSearch = true;    
        }
    }

    private void Shoot()
    {
        
        Instantiate(projectile, shotPoint.position, Quaternion.Euler(0f, 0f, rotZ + offset));
    }
}
