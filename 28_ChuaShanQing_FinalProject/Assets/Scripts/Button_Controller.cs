﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Instruction()
    {
        SceneManager.LoadScene("InstructionsScene");
    }

    public void Credits()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void Back()
    {
        SceneManager.LoadScene("StartScene");
    }
}
