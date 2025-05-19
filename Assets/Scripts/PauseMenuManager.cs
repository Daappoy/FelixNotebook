using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    public GameObject MainMenuPanel;
    bool isPaused = false;
    bool escapeKeyPressed = false;
    void Start()
    {
        MainMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !escapeKeyPressed)
        {
            escapeKeyPressed = true;
            if(isPaused == false)
            {
                PauseGame();
            } else
            {
                ResumeGame();
            }
        }
        if(Input.GetKeyUp(KeyCode.Escape)){
            escapeKeyPressed = false;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        MainMenuPanel.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        MainMenuPanel.SetActive(false);
        isPaused = false;
    }
    
}
