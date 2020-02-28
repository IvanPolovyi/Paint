
using UnityEngine;

using Random = UnityEngine.Random;
using Slider = UnityEngine.UI.Slider;

public class PlayerMove : MonoBehaviour
{
    public Animator animator;
    public float moveVel;
    public float timeBetweenDamage=1;
    public int health=10;
    public Slider healthBar;
    public float dashTime = 0.2f;
    public float timeBetweenDash=3f;
    public float dashVel=70;
    public LayerMask whatIsWall;
    public Animator cameraAnimator;
    public SceneTransition transition;

    private float timeSinceLastDamage;
    private float timeSinceLastDash;
    
    private Weapon weapon;
    private Vector2 movement;
    private bool dashing;
    private float thisDashTime;
    private Rigidbody2D rb;
    private Vector2 dashMovement = Vector2.zero;
    public float castDist = 2f;
    private TrailRenderer trails;
    public bool paused;
    
    private void Start()
    {
        weapon = gameObject.GetComponentInChildren<Weapon>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        trails = gameObject.GetComponent<TrailRenderer>();
    }


    void FixedUpdate()
    {
        if(paused) return;
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.position += moveVel * Time.deltaTime * movement;

   
        timeSinceLastDash += Time.deltaTime;
        if (Input.GetMouseButtonDown(1))
        {
            if (timeSinceLastDash >= timeBetweenDash && !dashing)
            {
                dashMovement = movement;
                dashing = true;
                timeSinceLastDash = 0;
                thisDashTime = dashTime;
                int r = Random.Range(0, 3);
                
                if(r == 0)
                    cameraAnimator.SetTrigger("Shake1");
                if(r == 1)
                    cameraAnimator.SetTrigger("Shake2");
                if(r == 2)
                    cameraAnimator.SetTrigger("Shake3");
            }
        }

        if (timeSinceLastDash >= timeBetweenDash)
        {
            trails.enabled = false;
        }
        else
        {
            trails.enabled = true;
        }

        if (dashing)
        {
            rb.position += dashVel * Time.deltaTime * dashMovement;
            thisDashTime -= Time.deltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, dashMovement, castDist, whatIsWall);
            Debug.DrawRay(transform.position, movement*castDist, Color.cyan, 1f);
            if (hit2D.collider != null && hit2D.collider.gameObject.CompareTag("Environment"))
            {
                dashing = false;
            }
        }

        if (dashing && thisDashTime <= 0)
        {
            dashing = false;
        }
        
        healthBar.value = health;
        if (health <= 0)
        {
            StartCoroutine(transition.LoadScene("YouDead", 1f));
        }
        timeSinceLastDamage += Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        
        if (timeSinceLastDamage >= timeBetweenDamage)
        {
            animator.SetTrigger("TakeDamage");
            health -= damage;
            timeSinceLastDamage = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        dashing = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PaintVial"))
        {
            weapon.setPaintReserve(30);
            other.gameObject.GetComponent<PaintVial>().OnDestroy();
        }

    }
    
}
