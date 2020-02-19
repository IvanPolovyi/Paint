using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public Animator animator;
    public float moveVel;

    public int health=5;
    public Slider healthBar;

    
    private Weapon weapon;
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Magnitude = Animator.StringToHash("Magnitude");
    private Vector3 movement; 
    
    private void Start()
    {
        weapon = gameObject.GetComponentInChildren<Weapon>();
//        dash.onClick.AddListener(Dash);
    }


    void Update()
    {

        movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical") , 0.0f);
        transform.position += moveVel * Time.deltaTime * movement;


        healthBar.value = health;
        if (health <= 0)
        {
           
        }
    }

    private void Dash()
    {
        transform.position += 1000 * Time.deltaTime * movement;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PaintVial"))
        {
            weapon.setPaintReserve(20);
            other.gameObject.GetComponent<PaintVial>().OnDestroy();
        }

        else if (other.CompareTag("Enemy"))
        {
            health--;
        }
    }
    
}
