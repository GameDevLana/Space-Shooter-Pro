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
    [SerializeField]
    private GameObject _enemyAvoidPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    
    [SerializeField]
    private GameObject[] enemyPrefabs;
    [SerializeField]
    private GameObject[] powerups;

    [SerializeField]
    private GameObject _bossPrefab;

    private bool _stopSpawning = false;

    public int[] enemyProb =
    {
        10,
        15,
        15,
        25,
        15,
        5,
        10,
        5,
    };
    public int total;
    public int randomNumber;

                                                //*****Add text for each wave, final wave & Boss intro.*******
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
    //StartCoroutine(SpawnEnemyFleetRoutine()); //****Instantiate fleetwaves of miniships from SpaceStation.*****

    /*                                  ********************Spawn MiniFleets**************
     * IEnumerator SpawnEnemyFleetRoutine()
     * {
     *  yield return new WaitForSeconds(2.0f);
     *  while (_stopSpawning == false)
     *  for (int waveNumber = 0; waveNumber < _enemiesPerWave.Length; waveNumber++)
     *  {
     *      int totalEnemies = _enemiesPerWave[waveNumber];
     *      for (int i = 0; i < totalEnemies; i++)
     *      {
     *          Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
     *          int randomEnemy = Random.Range(0, enemyPrefabs.Length);
                GameObject newEnemy = Instantiate(ene3myPRefabs[randomEnemy], posToSpawn, Quaternion. identity
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(3.0f);
            }
        }
       } 
     * 
     * 
     * 
     * 
     * 
     * 
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
                    int randomEnemy = Random.Range(0, enemyPrefabs.Length);
                    GameObject newEnemy = Instantiate(enemyPrefabs[randomEnemy], posToSpawn, Quaternion.identity);
                    newEnemy.transform.parent = _enemyContainer.transform;
                    yield return new WaitForSeconds(3.0f);
                    /* GameObject newEnemy;
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

           }*/
           while (GameObject.FindWithTag("Enemy") != null)
                {
                    yield return null;
                }
           yield return new WaitForSeconds(5f);
        }
        Debug.Log("All waves completed! Preparing for Boss");

        Vector3 bossSpawnPos = new(-11, 13, 0);
        Instantiate(_bossPrefab, bossSpawnPos, Quaternion.identity);
        _stopSpawning = true;
    }
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

    IEnumerator SpawnPowerupRoutine()                                      //**********ADD HOMINGPROJECTILE POWERUP TO SPAWN MGR   --changed to powerups.Length. 
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, powerups.Length);
            Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
   /* for (int i = 0; i < powerups.Length; i++)  
    {
        int randomPowerUp = powerups[i];
    }*////////////////////////////////////////////////////////////////////////////



    public void OnPlayerDeath()
    { 
    _stopSpawning = true;
    }
}
