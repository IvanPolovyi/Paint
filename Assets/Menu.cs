using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public SceneTransition sceneTransition;

    public GameObject UIImage;
    public GameObject resBut;
    public GameObject MMbut;
    public PlayerMove pl;
    public Weapon wp;

    private Color c;
    void Start()
    {
        UIImage.SetActive(false);
        resBut.SetActive(false);
        MMbut.SetActive(false);

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuBut();
        }
    }

    public void MenuBut()
    {
        Time.timeScale = 0;
        pl.paused = true;
        wp.paused = true;
        UIImage.SetActive(true);
        resBut.SetActive(true);
        MMbut.SetActive(true);
    }

    public void ResumeBut()
    {
        Time.timeScale = 1f;
        pl.paused = false;
        wp.paused = false;    
        UIImage.SetActive(false);
        resBut.SetActive(false);
        MMbut.SetActive(false);
    }

    public void MainMenuBut()
    {
        Time.timeScale = 1f;
        StartCoroutine(sceneTransition.LoadScene("MenuScene", 0.5f));
    }

}
