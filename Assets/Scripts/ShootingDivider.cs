using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDivider : Equipment
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 50f;
    public float cooldownTime = 0.5f; // Cooldown duration
    private float lastShootTime = -Mathf.Infinity;
    public EquipmentManager equipmentManager;

    void Start()
    {
        equipmentManager = FindObjectOfType<EquipmentManager>(); // Ensure equipmentManager is set
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= lastShootTime + cooldownTime && equipmentManager.currentEquipmentIndex == 2)
        {
            Use();
        }
    }

    public override void Use()
    {
        Shoot();
    }

    void Shoot()
    {
        // Instantiate and launch the bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        // Update the last shoot time
        lastShootTime = Time.time;
    }

    void OnEnable()
    {
        Debug.Log("ShootingDivider enabled");
    }

    void OnDisable()
    {
        Debug.Log("ShootingDivider disabled");
    }
}