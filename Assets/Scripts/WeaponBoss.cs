using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBoss : MonoBehaviour
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
    private Boss self;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        self = transform.parent.gameObject.GetComponent<Boss>();
    }

    private void Update()
    {
        if (self.dead) return;
        
        Vector3 difference = target.position - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        _timeSinceLastShoot += Time.deltaTime;
        
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, ( target.position - transform.position), 200f, whatIsPlayer);
        if (hit2D.collider != null)
        {
            if (_timeSinceLastShoot >= shootRate
                && hit2D.collider.gameObject.CompareTag("Player"))
            {
                _timeSinceLastShoot = 0;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Instantiate(projectile, shotPoint.position, Quaternion.Euler(0f, 0f, rotZ + offset-15f));
        Instantiate(projectile, shotPoint.position, Quaternion.Euler(0f, 0f, rotZ + offset));
        Instantiate(projectile, shotPoint.position, Quaternion.Euler(0f, 0f, rotZ + offset+15f));
    }
}