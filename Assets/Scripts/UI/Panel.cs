using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public Animator animator;

    public void GameOver()
    {
        animator.SetTrigger("GameOver");
    }
}
