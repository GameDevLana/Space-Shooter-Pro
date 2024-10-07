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
        StartCoroutine(SpawnEnemyRoutine());    //randomized instantiation for enemysheildprefab to be childed to all enemy types 
        StartCoroutine(SpawnPowerupRoutine());  //consider adding a mystery powerup that can be really special or negative = randomly
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
                float randomValue = Random.Range(0f, 1f);  // Generate a random value between 0 and 1

                if (randomValue > 0.66f)
                {
                    newEnemy = Instantiate(_enemyStraightPrefab, posToSpawn, Quaternion.identity);
                }
                else if (randomValue > 0.33f)
                {
                    newEnemy = Instantiate(_enemyDiagPrefab, posToSpawn, Quaternion.identity);
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

          /*  
             // Optional: Add a short delay between enemy spawns in the same wave

              yield return new WaitForSeconds(0.5f);
        
             // Wait for all enemies to be destroyed before starting the next wave

             while (GameObject.FindWithTag("Enemy") != null)
            {
            yield return null;
             }

            // Optional: Add a delay between waves

             yield return new WaitForSeconds(5f); // Wait 5 seconds between waves

             Debug.Log("All waves completed!");
         */

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
    /*    use powerups.Length instead of Random.Range(0, 70);
        for (int i = 0; i < powerups.Length; i++)  
            {
                int randomPowerUp = powerups[i];
            }*/
    public void OnPlayerDeath()
    { 
    _stopSpawning = true;
    }
}
