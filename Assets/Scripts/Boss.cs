using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    private Animator animator;
    public GameObject deathEffect;
    public GameObject deathSplash;
    private Transform target;
    private Stage stage;
    public int health;
    public bool dead;
    private Rigidbody2D rb;
    public SceneTransition sceneTransition;
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public enum Stage
    {
        Stage1,
        Stage2
    }
    void Update()
    {
        if(dead) return;

        rb.position += Vector2.right;
        
        if (health <= 0)
        {
            dead = true;
            Invoke("DeathSplash", 0.7f);
            animator.SetTrigger("BossDeath");
            NextLevel();

        }
    }
    void NextLevel()
    {
        StartCoroutine(sceneTransition.LoadScene("WinScene", 1.5f));
    }
    
    void DeathSplash()
    {
        Instantiate(deathSplash, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(-180,180)));
        Camera.main.GetComponent<RipplePostProcessor>().RippleEffect(gameObject.transform.position);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
