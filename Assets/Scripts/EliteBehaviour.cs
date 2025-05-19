using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EliteBehaviour : MonoBehaviour
{
    public StatsManager stats;
    public EliteBehaviour EliteEnemy;
    public Animator animator;
    public Transform Target;
    NavMeshAgent agent; // Reference to NavMeshAgent
    public PlayerEXPHP player;
    public string bullet = "bullet";
    public string playerID = "Player";
    public string BulletDivider = "BulletDivider";
    public float health = 200f;
    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<StatsManager>();
        
        EliteEnemy = FindObjectOfType<EliteBehaviour>();
        animator = EliteEnemy.GetComponent<Animator>();

        player = FindObjectOfType<PlayerEXPHP>();
        Target = player.GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
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
            health = health / 4;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        if(agent.isOnNavMesh)
        {
            agent.SetDestination(Target.position);
        }
    }

    void OnDestroy()
    {
        if(GameObject.FindGameObjectWithTag("WaveSpawner") != null)
        {
            GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>().spawnedEnemies.Remove(gameObject);
        }
        Debug.Log("Elite Enemy Destroyed, granting 40 EXP to player");
        player.GainEXP(40);
        stats.Elitekills++;
    }
}
