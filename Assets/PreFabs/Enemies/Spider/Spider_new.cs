using UnityEngine;
using UnityEngine.SceneManagement;
using Application = UnityEngine.Application;

public class Spider_new : MonoBehaviour
{
    public float xSpeed=1;
    public float ySpeed=1;
    public LayerMask whatIsGround;
   // public Stats stats;
    
    public GameObject[] frontLegs;
    public GameObject[] backLegs;
    //public GameObject player;

    public float stepRate = 0.6f;
    private float _timeSinceLastStep = 0;
    private int _curFLeg=0;
    private int _curBLeg=0;
    private bool _useFront;

    public float backCheckerOffset;
    public float frontCheckerOffset;
    public float hight = 1.5f;
    public Transform caseTarget;
    private Rigidbody2D _rb;
    
    private RaycastHit2D _highMidCheck;
    private RaycastHit2D _lowMidCheck;
    private RaycastHit2D _frontCheck;
    private RaycastHit2D _backCheck;

    private float lastTime;

    private void Start()
    {
        _rb = gameObject.GetComponentInChildren<Rigidbody2D>();
       // stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();
        
        for (int i = 0; i < 8; i++)
        {
            Step();
        }
        lastTime = Time.time;
    }

    private void Update()
    {
        _rb.position = transform.position;
        
        CastChecks();
        _timeSinceLastStep += Time.deltaTime;
        
        if (_timeSinceLastStep >= stepRate)
        {
            _timeSinceLastStep = 0;
            Step();
        }

        Vector2 moveVec;
        if ((transform.position - caseTarget.position).x > 0)
        {
            moveVec = new Vector2(-xSpeed,0);
        }
        else moveVec = new Vector2(xSpeed,0);
        
        
        
        if (_highMidCheck.collider != null)
        {
            moveVec.y = ySpeed;
        }
        else if (_lowMidCheck.collider == null)
        {
            moveVec.y = -ySpeed;
        }
        transform.position += new Vector3(moveVec.x, moveVec.y,0) * Time.deltaTime;
    }

    void Step()
    {
        GameObject leg;
        RaycastHit2D sensor;
        Vector2 target;
            
      //  if (_useFront)
        {
            leg = frontLegs[_curFLeg];
            _curFLeg = _curFLeg+1;
            if (_curFLeg >= frontLegs.Length) _curFLeg = 0;
            sensor = _frontCheck;
        }
      //  else
        target = sensor.point;
        leg.GetComponent<Leg>().Step(target);
        {
            leg = backLegs[_curBLeg];
            _curBLeg = _curBLeg + 1;
            if (_curBLeg >= backLegs.Length) _curBLeg = 0;
            sensor = _backCheck;
        }

        _useFront = !_useFront;
        
        

        CheckTime();
        if ((transform.position - caseTarget.position).magnitude < 4f*transform.localScale.x && canIHitPlayer())
        {
            target = caseTarget.position;
            playerHit++;
        }
        else target = sensor.point;
        leg.GetComponent<Leg>().Step(target);
    }

    
    void CheckTime()
    {
        if (Time.time - lastTime < 1)
            return;
        lastTime = Time.time;
        playerHit=0;
    }

    private int playerHit;
    bool canIHitPlayer()
    {
        if (playerHit > 2)
        {
            return false;
        }

        return true;
    }

    private void CastChecks()
    {
        _lowMidCheck = Physics2D.Raycast(transform.position, Vector2.down, hight*1.08f*transform.localScale.x, whatIsGround);
        Debug.DrawRay(transform.position, Vector2.down*2.1f, Color.blue, 0.1f, false);
        
        _highMidCheck = Physics2D.Raycast(transform.position, Vector2.down, hight*0.92f*transform.localScale.x, whatIsGround);
        Debug.DrawRay(transform.position, Vector2.down*1.9f, Color.red, 0.1f, false);

        _frontCheck = Physics2D.Raycast(transform.position+new Vector3(backCheckerOffset,0,0), Vector2.down, 10f, whatIsGround);
        Debug.DrawRay(transform.position+new Vector3(backCheckerOffset,0,0),Vector2.down*10f, Color.yellow, 0.1f, false);
        
        _backCheck = Physics2D.Raycast(transform.position+new Vector3(-frontCheckerOffset,0,0), Vector2.down, 10f, whatIsGround);
        Debug.DrawRay(transform.position+new Vector3(-frontCheckerOffset,0,0), Vector2.down*20f,Color.green, 0.1f, false);
    }
}
