using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Mathematics mathScript;
    public Slider TimerBar;
    public float timer = 10;
    private int waitSecInt;
    
    public void Update()
    {
        // Debug.Log("test");
        
        if(timer > 0 && mathScript.isMathActive == true)
        {
            Debug.Log("Timer is on for " + timer + " seconds.");
            UpdateTimerBar();
            timer -= Time.unscaledDeltaTime;
            waitSecInt = (int)timer;
            Debug.Log(waitSecInt);
        }
        else if(timer <= 0)
        {
            Debug.Log("Time's up!");
            mathScript.Skip();
            mathScript.isMathActive = false;
            timer = 10;
        }
        else if (mathScript.isMathActive == false)
        {
            timer = 10;
        }
    }

    private void UpdateTimerBar()
    {
        TimerBar.value = timer / 10;
    }
}
