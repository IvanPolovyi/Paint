using UnityEngine;


public class EnemyOrange : MonoBehaviour
{
    public Animator animator;
    public int health;
    private Weapon_enemy weaponEnemy;

    public GameObject deathEffect;
    private RipplePostProcessor camRipple;
    private float _timeSinceLastShoot = 0;
    
    private void Start()
    {
        
        weaponEnemy = gameObject.GetComponentInChildren<Weapon_enemy>();  
    }

    private void Update()
    {
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(-180,180)));
            Destroy(transform.parent.gameObject);
            Camera.main.GetComponent<RipplePostProcessor>().RippleEffect(gameObject.transform.position);
        }

    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }
}


