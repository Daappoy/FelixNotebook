using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnBehaviour : MonoBehaviour
{
    public StatsManager stats;
    public EnBehaviour enemy;
    public Animator animator;
    public Transform Target;
    NavMeshAgent agent;
    public PlayerEXPHP player;
    public string bullet = "bullet";
    public string playerID = "Player";
    public string BulletDivider = "BulletDivider";
    public float health = 100f;

    void Start()
    {   
        stats = FindObjectOfType<StatsManager>();

        enemy = FindObjectOfType<EnBehaviour>();
        animator = enemy.GetComponent<Animator>();

        player = FindObjectOfType<PlayerEXPHP>();
        Target = player.GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(bullet))
        {
            health -= player.damage;
        }
        if (collision.gameObject.CompareTag(BulletDivider))
        {
            health = health / 2;
        }
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(Target.position);
        }
    }

    void OnDestroy()
    {
        if (GameObject.FindGameObjectWithTag("WaveSpawner") != null)
        {
            GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>().spawnedEnemies.Remove(gameObject);
        }
        Debug.Log("Enemy destroyed, granting 15 EXP to player");
        player.GainEXP(15);
        stats.gruntkills++;
    }
}