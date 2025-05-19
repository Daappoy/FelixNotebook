using System.Collections;
using UnityEngine;

public class Shooting : Equipment
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject fireRateDebuffindicator;

    public float bulletForce = 50f;
    public float cooldownTime = 0.5f; // Cooldown duration
    private float lastShootTime = -Mathf.Infinity;

    public Animator animator;
    public EquipmentManager equipmentManager;

    void Start()
    {
        equipmentManager = FindObjectOfType<EquipmentManager>(); // Ensure equipmentManager is set
        fireRateDebuffindicator.SetActive(false); //indicator off
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= lastShootTime + cooldownTime && equipmentManager.currentEquipmentIndex == 0)
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
        animator.SetTrigger("Shoot"); // Play the shooting animation
        
        // Instantiate and launch the bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        // Update the last shoot time
        lastShootTime = Time.time;
    }

    public void ApplyFireRateDebuff(float debuffDuration, float debuffAmount)
    {
        StartCoroutine(FireRateDebuffCoroutine(debuffDuration, debuffAmount));
    }

    private IEnumerator FireRateDebuffCoroutine(float debuffDuration, float debuffAmount)
    {
        float originalCooldownTime = cooldownTime;
        float nerfedcooldown = cooldownTime += debuffAmount; // Increase cooldown time to apply debuff
        Debug.Log("current firerate: " + nerfedcooldown);
        fireRateDebuffindicator.SetActive(true); //indicator on
        yield return new WaitForSeconds(debuffDuration);
        fireRateDebuffindicator.SetActive(false); //indicator off
        cooldownTime = originalCooldownTime; // Reset cooldown time after debuff duration
    }

    void OnEnable()
    {
        Debug.Log("Shooting enabled");
    }

    void OnDisable()
    {
        Debug.Log("Shooting disabled");
    }
}