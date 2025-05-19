using UnityEngine;

public class Multiplier : Equipment
{
    public PlayerEXPHP playerEXPHP;
    public EquipmentManager equipmentManager;

    void Start()
    {
        // playerEXPHP = FindObjectOfType<PlayerEXPHP>();
        // equipmentManager = FindObjectOfType<EquipmentManager>(); // Ensure equipmentManager is set
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (equipmentManager.currentEquipmentIndex == 3)
            {
                Use();
            }
        }
    }

    public override void Use()
    {
        MultiplyHealth();
    }

    void MultiplyHealth()
    {
        playerEXPHP.currentHealth = Mathf.Min(playerEXPHP.currentHealth * 2, playerEXPHP.maxHealth);
        playerEXPHP.UpdateHPBar();
    }

    void OnEnable()
    {
        Debug.Log("HealthMultiplier enabled");
    }

    void OnDisable()
    {
        Debug.Log("HealthMultiplier disabled");
    }
}