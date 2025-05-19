using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class PlayerEXPHP : MonoBehaviour
{
    public SceneManager scene;
    public Slider hpBar;   // Reference to HP bar slider
    public Slider expBar;  // Reference to EXP bar slider

    public HealingEquipment healingEquipment;
    public GameObject healthregenindicator;
    public GameObject damageupgradeindicator;
    public GameObject healthMultiplierIndicator; // Add new indicator
    public GameObject damageDebuffindicator;

    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 50;
    public int maxEXP = 100;
    public int currentEXP;

    public int level = 1;

    private bool isHealthRegenActive = false;
    private bool isTouchingEnemy = false;
    private bool isTouchingElite = false;
    private int healthRegenRate = 5; // Health regen per second

    void Start()
    {
        damageDebuffindicator.SetActive(false);
        healthregenindicator.SetActive(false);
        damageupgradeindicator.SetActive(false);
        healthMultiplierIndicator.SetActive(false); // Add this line
        // Initialize stats
        currentHealth = maxHealth;
        currentEXP = 0;

        hpBar.maxValue = maxHealth;
        expBar.maxValue = maxEXP;
    }

    void FixedUpdate()
    {
        UpdateHPBar();
        UpdateEXPBar();
    }

    public void UpgradeDamage()
    {
        damage += 20; // Increase damage
        damageupgradeindicator.SetActive(true);
        StartCoroutine(ResetDamageAfterTime(15f));
    }

    private IEnumerator ResetDamageAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        damageupgradeindicator.SetActive(false);
        damage -= 20; // Reset damage
    }

    public void ApplyDamageDebuff(float debuffDuration, int debuffAmount)
    {
        StartCoroutine(DamageDebuffCoroutine(debuffDuration, debuffAmount));
    }

    private IEnumerator DamageDebuffCoroutine(float debuffDuration, int debuffAmount)
    {
        damage -= debuffAmount; // Decrease damage to apply debuff
        damageDebuffindicator.SetActive(true); //indicator on
        yield return new WaitForSeconds(debuffDuration);
        damageDebuffindicator.SetActive(false); //indicator off
        damage += debuffAmount; // Reset damage after debuff duration
    }

    public void ActivateHealthRegen()
    {
        isHealthRegenActive = true;
        healthregenindicator.SetActive(true);
        StartCoroutine(HealthRegenCoroutine());
        StartCoroutine(DeactivateHealthRegenAfterTime(15f));
    }

    private IEnumerator DeactivateHealthRegenAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        healthregenindicator.SetActive(false);
        isHealthRegenActive = false;
    }

    private IEnumerator HealthRegenCoroutine()
    {
        while (isHealthRegenActive)
        {
            yield return new WaitForSeconds(2f);
            RegenerateHealth();
        }
    }

    private void RegenerateHealth()
    {
        currentHealth = Mathf.Min(currentHealth + healthRegenRate, maxHealth);
        UpdateHPBar();
    }

    public void selfHeal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healingEquipment.healAmount, maxHealth);
        UpdateHPBar();
    }

    public void UpdateHPBar()
    {
        hpBar.value = (float)currentHealth;
    }

    void UpdateEXPBar()
    {
        expBar.value = (float)currentEXP;
    }

    public void GainEXP(int exp)
    {
        currentEXP += exp;
        if (currentEXP >= maxEXP)
        {
            naiklevel();
        }
    }

    [ContextMenu("naiklevel")]
    private void naiklevel()
    {
        Debug.Log("naiklevel");
        level++;
        currentEXP = currentEXP % maxEXP;
        maxEXP = maxEXP + 50;
        maxHealth = maxHealth + 20;
        currentHealth = maxHealth;
        damage = damage + 10;

        hpBar.maxValue = maxHealth;
        expBar.maxValue = maxEXP;

        UpdateHPBar();
        UpdateEXPBar();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            isTouchingEnemy = true;
            StartCoroutine(DecreaseHealthOverTime());
        }
        else if (collision.gameObject.CompareTag("Elite"))
        {
            isTouchingElite = true;
            StartCoroutine(DecreaseHealthOverTime());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("Elite"))
        {
            isTouchingEnemy = false;
            isTouchingElite = false;
        }
    }

    private IEnumerator DecreaseHealthOverTime()
    {
        while (isTouchingEnemy || isTouchingElite)
        {
            if(isTouchingEnemy)
            {
                DecreaseHealth(15);
                yield return new WaitForSeconds(1.5f);
            }
            else if(isTouchingElite)
            {
                DecreaseHealth(30);
                yield return new WaitForSeconds(2f);
            }
        }
    }

    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            // Handle player death (e.g., trigger game over)
            SceneManager.LoadScene("GameOver");
        }
        UpdateHPBar();
    }
}