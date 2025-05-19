using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PostRoundBuff : MonoBehaviour
{
    public GameObject upgradePanel;
    // Start is called before the first frame update
    void Start()
    {
        upgradePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void upgrade()
    {
        upgradePanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
