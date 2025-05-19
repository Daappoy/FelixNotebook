using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject movementUpgradeIndicator;
    public float moveSpeed = 5f;  // Movement speed of the player
    private Rigidbody2D rb;
    public Camera cam;
    Vector2 mousePos;
    public Animator animator;
    Vector2 movement;
    public GameObject speedDebuffindicator;

    void Start()
    {
        movementUpgradeIndicator.SetActive(false);
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component
        speedDebuffindicator.SetActive(false);
    }

    void Update()
    {
        if(movement.x != 0 || movement.y != 0)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
        // Get input from the player
        movement.x = Input.GetAxisRaw("Horizontal");  // Left/Right (A/D or Arrow Keys)
        movement.y = Input.GetAxisRaw("Vertical");    // Up/Down (W/S or Arrow Keys)

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        // Normalize the movement vector to prevent faster diagonal movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 LookDir = mousePos - rb.position;
        float Angle = (Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg) - 90f;
        rb.rotation = Angle;
    }

    public void ApplySpeedDebuff(float debuffDuration, float debuffAmount)
    {
        StartCoroutine(SpeedDebuffCoroutine(debuffDuration, debuffAmount));
    }

    private IEnumerator SpeedDebuffCoroutine(float debuffDuration, float debuffAmount)
    {
        moveSpeed -= debuffAmount; // Decrease speed to apply debuff
        speedDebuffindicator.SetActive(true);
        yield return new WaitForSeconds(debuffDuration);
        speedDebuffindicator.SetActive(false);
        moveSpeed += debuffAmount; // Reset speed after debuff duration
    }

    public void UpgradeSpeed()
    {
        moveSpeed += 2f; // Increase speed
        movementUpgradeIndicator.SetActive(true);
        StartCoroutine(ResetSpeedAfterTime(15f));
    }

    private IEnumerator ResetSpeedAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        movementUpgradeIndicator.SetActive(false);
        moveSpeed -= 2f; // Reset speed
    }
}