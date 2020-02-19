using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{

    public Animator animator;
    public string sceneName;
    public Button startButton;
    public Button exitButton;
    public Button menuButton;

    private void Start()
    {
        
        if(menuButton != null) menuButton.onClick.AddListener(MenuButtonPressed);
        
        if(startButton != null) startButton.onClick.AddListener(StartButtonPressed);
        
        if(exitButton != null) exitButton.onClick.AddListener(ExitButtonPressed);
        
    }

    private void MenuButtonPressed()
    {
        StartCoroutine(LoadScene());
    }
    private void StartButtonPressed()
    {
        StartCoroutine(LoadScene());
    }
    
    private static void ExitButtonPressed()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LoadScene());
        }
        
    }

    IEnumerator LoadScene()
    {
        animator.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
