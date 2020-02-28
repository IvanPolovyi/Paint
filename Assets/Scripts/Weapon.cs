using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Weapon : MonoBehaviour
{
    public float offset;
    public GameObject projectile;
    public Transform shotPoint;
    public Slider paintReserveBar;
    public Button button;
    public Joystick joystick;
    public float startTimeBtwShots;

    private GameObject target;
    public int paintReserve;

    private float timeBtwShots;
    private float rotZ;
    public bool paused;

    private void Start()
    {
       
    }

    private void Update()
    {
        if(paused) return;
        
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        
        paintReserveBar.value = paintReserve;
    }

    private void Shoot()
    {
        if(paintReserve<=0) return;
        Instantiate(projectile, shotPoint.position, Quaternion.Euler(0f, 0f, rotZ + offset));
            timeBtwShots = startTimeBtwShots;
            paintReserve--;
    }

    public void setPaintReserve(int reserve)
    {
        paintReserve += reserve;
    }
}