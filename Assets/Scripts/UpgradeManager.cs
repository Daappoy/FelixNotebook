using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradePanel;
    public Button speedUpgradeButton;
    public Button damageUpgradeButton;
    public Button healthRegenUpgradeButton;

    private PlayerMovement playerMovement;
    private PlayerEXPHP playerEXPHP;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerEXPHP = FindObjectOfType<PlayerEXPHP>();

        // speedUpgradeButton.onClick.AddListener(OnSpeedUpgrade);
        // damageUpgradeButton.onClick.AddListener(OnDamageUpgrade);
        // healthRegenUpgradeButton.onClick.AddListener(OnHealthRegenUpgrade);

        upgradePanel.SetActive(false);
    }

    public void ShowUpgradePanel()
    {
        upgradePanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game when the upgrade panel is shown
    }

    public void OnSpeedUpgrade()
    {
        playerMovement.UpgradeSpeed();
        Debug.Log("Upgrade chosen: Speed. Value: +2f");
        upgradePanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }

    public void OnDamageUpgrade()
    {
        playerEXPHP.UpgradeDamage();
        Debug.Log("Upgrade chosen: Damage. Value: +20");
        upgradePanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }

    public void OnHealthRegenUpgrade()
    {
        playerEXPHP.ActivateHealthRegen();
        Debug.Log("Upgrade chosen: Health Regeneration. Duration: 90 seconds");
        upgradePanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }
}