using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{

    public Animator animator;

    public IEnumerator LoadScene(String sceneName, float waitTime=1f, String trigger = "end")
    {
        animator.SetTrigger("end");
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneName);
    }
}
