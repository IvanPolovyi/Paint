
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashColor : MonoBehaviour
{
    private Color color;
    private void Start()
    {

        float rand = Random.Range(0.0f, 0.15f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(rand,rand,Random.Range(0.5f,1.0f));

    }

    public void setColor(Color color)
    {
        this.color = color;
    }
    

}
    