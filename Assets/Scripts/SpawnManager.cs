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
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
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
                GameObject newEnemy; if (Random.Range(0, 1f) > 0.5)
                {
                    newEnemy = Instantiate(_enemyStraightPrefab, posToSpawn, Quaternion.identity);
                }
                else
                {
                    newEnemy = Instantiate(_enemyDiagPrefab, posToSpawn, Quaternion.identity);
                }
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(3.0f);
           }

           yield return new WaitForSeconds(5f);
        }
    }

    //randomly assign shield to enemy spawns 


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
