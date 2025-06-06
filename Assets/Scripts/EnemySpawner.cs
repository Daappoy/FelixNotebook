using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 
public class WaveSpawner : MonoBehaviour
{
    public StatsManager stats;
    public Mathematics Mathematics;
    public GameObject EnemycounterImage;
    public TextMeshProUGUI Enemycounter;
    public GameObject WaveImage;
    public TextMeshProUGUI WaveCounter;
    public List<Enemy> enemies = new List<Enemy>();
    public int currWave =1;
    private int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
 
    public Transform[] spawnLocation;
    public int spawnIndex;
    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;
    private bool DuringWave = false;
 
    public List<GameObject> spawnedEnemies = new List<GameObject>();
    // Start is called before the first frame update
    
    void Start()
    {
        // Enemycounter.text = "" + spawnedEnemies.Count;
        spawnIndex = Random.Range(0,16);
        GenerateWave();
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        Enemycounter.text = "" + spawnedEnemies.Count;
        WaveCounter.text = "" + currWave;
        if(spawnTimer <=0)
        {
            //spawn an enemy
            if(enemiesToSpawn.Count >0)
            {
                DuringWave = true;
                GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[0], spawnLocation[spawnIndex].position,Quaternion.identity); // spawn first enemy in our list
                
                enemiesToSpawn.RemoveAt(0); // and remove it
                spawnedEnemies.Add(enemy);
                spawnTimer = spawnInterval;
 
                if(spawnIndex + 1 <= spawnLocation.Length-1)
                {
                    spawnIndex++;
                }
                else
                {
                    spawnIndex = 0;
                }
            }
            else
            {  
                // EnemycounterImage.SetActive(false);
                waveTimer = 0; // if no enemies remain, end wave
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }
 
        if(waveTimer<=0 && spawnedEnemies.Count <=0)
        {
            //wave ended
            DuringWave = false;
            currWave++;
            stats.roundstat++;
            Debug.Log("round ended, onto the next one");
            Mathematics.StartMathQuestion();

            GenerateWave();
        }

        if(DuringWave)
        {
            EnemycounterImage.SetActive(true);
            WaveImage.SetActive(true);
        }
        else
        {
            EnemycounterImage.SetActive(false);
            WaveImage.SetActive(false);
        }
    }
 
    public void GenerateWave()
    {
        waveValue = currWave * 3;
        GenerateEnemies();
 
        spawnInterval = waveDuration / enemiesToSpawn.Count; // gives a fixed time between each enemies
        waveTimer = waveDuration; // wave duration is read only
    }
 
    public void GenerateEnemies()
    {
        // Create a temporary list of enemies to generate
        // 
        // in a loop grab a random enemy 
        // see if we can afford it
        // if we can, add it to our list, and deduct the cost.
 
        // repeat... 
 
        //  -> if we have no points left, leave the loop
 
        List<GameObject> generatedEnemies = new List<GameObject>();
        while(waveValue>0 || generatedEnemies.Count <50)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;
 
            if(waveValue-randEnemyCost>=0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if(waveValue<=0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }
  
}
 
[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}