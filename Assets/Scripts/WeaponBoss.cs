using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBoss : MonoBehaviour
{
    public float offset;
    public GameObject projectile;
    public Transform shotPoint;
    public float shootRate = 2f;
    public LayerMask whatIsSolid;
    
    private Transform _target;
    private float _timeBtwShots;
    private float _rotZ;
    private float _timeSinceLastShoot;
    private GreenBoss _self;

    private void Start()
    {
        _target = GameObject.FindWithTag("Player").transform;
        _self = transform.parent.gameObject.GetComponent<GreenBoss>();
    }

    private void Update()
    {
        if (_self.dead) return;
        
        Vector3 difference = _target.position - transform.position;
        _rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, _rotZ + offset);
        _timeSinceLastShoot += Time.deltaTime;
        
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, ( _target.position - transform.position), 200f, whatIsSolid);
        if (hit2D.collider != null)
        {
            if (_timeSinceLastShoot >= shootRate && hit2D.collider.gameObject.CompareTag("Player"))
            {
                _timeSinceLastShoot = 0;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Instantiate(projectile, shotPoint.position, Quaternion.Euler(0f, 0f, _rotZ + offset-15f));
        Instantiate(projectile, shotPoint.position, Quaternion.Euler(0f, 0f, _rotZ + offset));
        Instantiate(projectile, shotPoint.position, Quaternion.Euler(0f, 0f, _rotZ + offset+15f));
    }
}