using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPaintSplatter : MonoBehaviour
{
    public Button button;
    private Animator animator;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        button.onClick.AddListener(StartClicked);
    }

    private void StartClicked()
    {
        animator.SetTrigger("StartClicked");
    }
    
    void Update()
    {
        
    }
}
