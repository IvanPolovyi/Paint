using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SceneTransition transition;
    private void Start()
    {
        
    }


    public void StartBut()
    {
        StartCoroutine(transition.LoadScene("Level0Scene", 1f));
    }
    
    public void ExitBut()
    {
        Application.Quit();
    }

    public void CreditsBut()
    {
        StartCoroutine(transition.LoadScene("Credits", 0.5f));
    }
    public void MainMenuBut()
    {
        Time.timeScale = 1f;
        StartCoroutine(transition.LoadScene("MenuScene", 0.5f));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Update is called once per fram
}
