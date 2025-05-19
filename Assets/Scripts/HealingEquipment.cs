using System.Collections;
using UnityEngine;

public class HealingEquipment : Equipment
{
    public int healAmount = 30;
    public float cooldownTime = 15f; // Cooldown duration
    private float lastHealTime = -Mathf.Infinity;

    public PlayerEXPHP playerEXPHP;
    public EquipmentManager equipmentManager;

    void Start()
    {
        playerEXPHP = FindObjectOfType<PlayerEXPHP>();
        equipmentManager = FindObjectOfType<EquipmentManager>(); // Ensure equipmentManager is set
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time >= lastHealTime + cooldownTime && equipmentManager.currentEquipmentIndex == 1)
            {
                Use();
            }
        }
    }

    public override void Use()
    {
        Heal();
    }

    void Heal()
    {
        playerEXPHP.selfHeal(healAmount);
        lastHealTime = Time.time;
    }

    void OnEnable()
    {
        Debug.Log("HealingEquipment enabled");
    }

    void OnDisable()
    {
        Debug.Log("HealingEquipment disabled");
    }
}