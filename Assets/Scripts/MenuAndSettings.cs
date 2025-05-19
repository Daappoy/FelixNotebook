using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAndSettings : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject SettingsPanel;
    
    


    //opening a settings panel
    public void MainMenuOpen()
    {
        MainMenuPanel.SetActive(true);
    }
    public void MainMenuClose()
    {
        MainMenuPanel.SetActive(false);
    }
    public void SettingsOpen()
    {
        SettingsPanel.SetActive(true);
    }
    public void SettingsClose()
    {
        SettingsPanel.SetActive(false);
    }

    //Quit
    public void Quit()
    {
        Application.Quit();
    }

}
