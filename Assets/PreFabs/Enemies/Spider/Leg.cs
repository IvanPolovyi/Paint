using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Leg : MonoBehaviour
{
    private const float MIN_DIST = 5f;
    private const float pi = Mathf.PI;

    public GameObject joint1;
    public GameObject joint2;
    public GameObject hand;

    private float _lenUpper;
    private float _lenMiddle;
    private float _lenLower;

    public bool flipped;
    private Vector2 _goalPos = new Vector2();
    private Vector2 _intPos = new Vector2();
    private Vector2 _startPos = new Vector2();
    private const float StepHeight = 0.5f;
    private const float StepRate = 0.2f;
    private float _stepTime = 0;
    public float _lenTotal;


    void Awake()
    {
        _lenUpper = (joint1.transform.position - transform.position).magnitude;
        _lenMiddle = (joint1.transform.position- joint2.transform.position).magnitude;
        _lenLower = (joint2.transform.position - hand.transform.position).magnitude;

        
        if (flipped)
        {
            joint1.GetComponent<SpriteRenderer>().flipY = !joint1.GetComponent<SpriteRenderer>().flipY;
            joint2.GetComponent<SpriteRenderer>().flipY = !joint2.GetComponent<SpriteRenderer>().flipY;
        }
    }
    void Update()
    {
        _stepTime += Time.deltaTime;
        var targetPos = new Vector2();
        var t = _stepTime / StepRate;
        if (t < 0.5f) targetPos = Vector2.Lerp(_startPos, _intPos, t / 0.5f);
        else if (t < 1) targetPos = Vector2.Lerp(_intPos, _goalPos, (t - 0.5f) / 0.5f);
        else targetPos = _goalPos;
        UpdateIk(targetPos);

    }

    public void Step(Vector2 gPos)
    {
        if (_goalPos == gPos) return;
        _goalPos = gPos;
        var handPos = hand.transform.position;
        
        var highest = _goalPos.y;
        if (handPos.y > highest) highest = handPos.y;

        var mid = (_goalPos.x + handPos.x) / 2;
        _startPos = handPos;
        _intPos = new Vector2(mid ,highest + StepHeight);
        _stepTime = 0;
    }
    void UpdateIk(Vector2 targetPos)
    {
        var offset = targetPos - (Vector2) transform.position;
        var disToTar = offset.magnitude;
        

        if (disToTar < MIN_DIST)    
        {
            offset = (offset / disToTar) * MIN_DIST;
            disToTar = MIN_DIST;
        }

        var baseR = Mathf.Atan2(offset.y, offset.x);
        _lenTotal = _lenLower + _lenMiddle + _lenUpper;
        var lenDummySide = (_lenUpper + _lenMiddle) * Mathf.Clamp(disToTar / _lenTotal, 0.0f, 1.0f);

        var baseAngles = SSS_calc(lenDummySide, _lenLower, disToTar);
        var nexAngles = SSS_calc(_lenUpper, _lenMiddle, lenDummySide);
        
        const float r2d = Mathf.Rad2Deg;

        if (disToTar >= _lenTotal)
        {
            transform.localEulerAngles = new Vector3(0, 0, baseR*r2d);
            joint1.transform.localRotation = Quaternion.identity;
            joint2.transform.localRotation = Quaternion.identity;
            return;
        }
        
        transform.localEulerAngles = new Vector3(0, 0, (baseAngles.y + nexAngles.y + baseR)*r2d);
        joint1.transform.localEulerAngles = new Vector3(0,0, nexAngles.z*r2d);
        joint2.transform.localEulerAngles = new Vector3(0, 0, (baseAngles.z + nexAngles.x)*r2d);
    }

    Vector3 SSS_calc(float sideA, float sideB, float sideC)
    {
        if(sideC>=sideA+sideB) return new Vector3(0,0,0);
        var angleA = LawOfCos(sideB, sideC, sideA);
        var angleB = LawOfCos(sideC, sideA, sideB) + pi;
        var angleC = pi -(angleA + angleB);
        //angleA = 0;
        if (flipped) return new Vector3(-angleA, -angleB, -angleC);
        return new Vector3(angleA, angleB, angleC);
    }

    private static float LawOfCos(float a, float b, float c)
    {
        if (2 * a * b == 0) return 0;
        return Mathf.Acos((a * a + b * b - c * c) / (2 * a * b));
    }
}
