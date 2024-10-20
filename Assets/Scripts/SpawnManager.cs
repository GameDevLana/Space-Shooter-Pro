using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyStraightPrefab;
    [SerializeField]
    private GameObject _enemyDiagPrefab;
    [SerializeField]
    private GameObject _enemyAggPrefab;
    [SerializeField]
    private GameObject _enemySmartPrefab;
                        //***SPAWN SMART ENEMY
                        //***SPAWN AVOID ENEMY
                        //***ENEMY TYPE ARRAY 
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] powerups;

    private bool _stopSpawning = false;

    public int[] table =
    {
        19,
        19,
        19,
        19,
        10,
        5,
        5,
    };
    public int total;
    public int randomNumber;
    [SerializeField]
    public int[] _enemiesPerWave = new int[]
    {
        5,
        10,
        15,
        20,
        25,
    };

    /*
    {
        _enemyStraightPrefab.GetComponent<Enemy>();
        _enemyDiagPrefab.GetComponent<Enemy2>();
        if (_enemyDiagPrefab == null)
        {
            Debug.LogError("Enemy is null");
        }    
        else if (_enemyStraightPrefab == null)
        {
            Debug.LogError("Enemy2 is null");
        }
    }
    */
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());    //***Randomized instantiation for enemysheildprefab to be childed to ALL enemy types?***
        StartCoroutine(SpawnPowerupRoutine());  //***Consider adding a mystery powerup that can be really special or negative = randomly.***
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopSpawning == false)
        for (int waveNumber = 0; waveNumber < _enemiesPerWave.Length; waveNumber++)
        {
           int totalEnemies = _enemiesPerWave[waveNumber];
           for (int i = 0; i < totalEnemies; i++)
           {
                Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                GameObject newEnemy;
                float randomValue = Random.Range(0f, 1f);  //** Update generate random value for each enemy type probability**

                if (randomValue > 0.66f)
                {
                    newEnemy = Instantiate(_enemyStraightPrefab, posToSpawn, Quaternion.identity);
                }
                else if (randomValue > 0.33f)
                {
                    newEnemy = Instantiate(_enemySmartPrefab, posToSpawn, Quaternion.identity);
                }
                else
                {
                    newEnemy = Instantiate(_enemyAggPrefab, posToSpawn, Quaternion.identity);
                }
                    yield return new WaitForSeconds(3.0f);
           }
           yield return new WaitForSeconds(5f);
        }
    }

                                 //********NEW SPAWN ENEMY ROUTINE USING ARRAY******* (see Powerup spawning behavior)
    /*
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemy], posToSpawn, Quatiernion.idnetity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
    }*/   

        /*                     // ***OPTIONAL: Add a short delay between enemy spawns in the same wave:****
        yield return new WaitForSeconds(0.5f);
            
                                // ***Wait for all enemies to be destroyed before starting the next wave:*****

        while (GameObject.FindWithTag("Enemy") != null)
        {
        yield return null;
        }
   
                                //****OPTIONAL: Add a delay between waves:*******

        yield return new WaitForSeconds(5f); // Wait 5 seconds between waves
        Debug.Log("All waves completed!");
    
         *//////////////////////////////////////////////////////

    IEnumerator SpawnPowerupRoutine()                                       
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, 7);
            Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
                            /*****USE powerups.length instead of Random.Range(0, 70);*******
    for (int i = 0; i < powerups.Length; i++)  
    {
        int randomPowerUp = powerups[i];
    }*////////////////////////////////////////////////////////////////////////////

    public void OnPlayerDeath()
    { 
    _stopSpawning = true;
    }
}
