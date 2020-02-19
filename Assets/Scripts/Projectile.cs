using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public GameObject destroyEffect;
    public float distance;
    public LayerMask whatIsSolid;
    public int damage;
    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }
    void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector2.up);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.gameObject.GetComponent<Enemy>().takeDamage(damage);
                DestroyProjectile();
            }
            else if (hitInfo.collider.CompareTag("Environment"))
            {
                DestroyProjectile();
            }
        }
    }

    private void DestroyProjectile()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
