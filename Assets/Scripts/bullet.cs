using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public GameObject hiteffect;
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hiteffect, transform.position, quaternion.identity);
        Destroy(effect, 0.30f);
        Destroy(gameObject);
        Debug.Log("Bullet collided with: " + collision.gameObject.name);
    }

    void Start()
    {
        Destroy(gameObject, 5);
    }
}
