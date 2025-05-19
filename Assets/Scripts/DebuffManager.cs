using System.Collections;
using UnityEngine;

public class DebuffManager : MonoBehaviour
{
    public GameObject debuffPanel;
    private PlayerMovement playerMovement;
    private PlayerEXPHP playerEXPHP;
    private Shooting shooting;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerEXPHP = FindObjectOfType<PlayerEXPHP>();
        shooting = FindObjectOfType<Shooting>();
        debuffPanel.SetActive(false);
    }

    public void ShowDebuffPanel()
    {
        debuffPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game when the debuff panel is shown
    }

    [ContextMenu("OnMovementDebuff")]
    public void OnMovementDebuff()
    {
        playerMovement.ApplySpeedDebuff(10f, 0.5f); // Apply speed debuff for 10 seconds, decreasing speed by 1.5f
        Time.timeScale = 1f; // Resume the game 
        debuffPanel.SetActive(false);  
    }

    [ContextMenu("OnDamageDebuff")]
    public void OnDamageDebuff()
    {
        playerEXPHP.ApplyDamageDebuff(10f, 15); // Apply damage debuff for 10 seconds, decreasing damage by 25
        Time.timeScale = 1f; // Resume the game
        debuffPanel.SetActive(false);
    }


    [ContextMenu("OnFireRateDebuff")]
    public void OnFireRateDebuff()
    {
        shooting.ApplyFireRateDebuff(10f, 0.5f); // Apply fire rate debuff for 10 seconds, increasing cooldown by 0.5 seconds
        Time.timeScale = 1f; // Resume the game
        debuffPanel.SetActive(false);
    }
}