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

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine(10, 3f));
        StartCoroutine(SpawnPowerupRoutine());  //consider adding a mystery powerup that can be really special or negative = randomly
    }

    IEnumerator SpawnEnemyRoutine(int enemies, float delayToSpawn)
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopSpawning == false)
        {
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
                enemies--;
                yield return new WaitForSeconds(3.0f);
            }
            //  StartCoroutine(EnemyWaveTwoRoutine(5, 3f));
        }
    }
    /*************************************************************************
  
    public int[] enemiesPerWave = new int[] { 5, 10, 15, 20, 25 }; // Number of enemies per wave

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(2.0f); 
        for (int waveNumber = 0; waveNumber < enemiesPerWave.Length; waveNumber++)
        {
            int totalEnemies = enemiesPerWave[waveNumber];  
            for (int i = 0; i < totalEnemies; i++)
            {
                float enemyPrefabToSpawn = Random.Range(0f, 1f);
                if (enemyPrefabToSpawn > 0.3f)
             
    
    {
                    Vector3 posToSpawn = new Vector3(Random.Range(0f, 14.5f), 11, 0);
                    GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
                    newEnemy.transform.parent = _enemyContainer.transform;
                }
                else
                {
                    Vector3 posToSpawn = new Vector3(-16.7f, Random.Range(2.0f, 6.5f), 0);
                    GameObject newEnemy = Instantiate(_enemy2Prefab, posToSpawn, Quaternion.identity);
                    newEnemy.transform.parent = _enemyContainer.transform;
                }

            // Optional: Add a short delay between enemy spawns in the same wave
              yield return new WaitForSeconds(0.5f);
            }
        
        // Wait for all enemies to be destroyed before starting the next wave
        while (GameObject.FindWithTag("Enemy") != null)
        {
            yield return null;
        }

        // Optional: Add a delay between waves
        yield return new WaitForSeconds(5f); // Wait 5 seconds between waves
    }

    Debug.Log("All waves completed!");
}

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    IEnumerator SpawnEnemyWave(int waveNumber)
    {
        int enemiesPerGroup = 5;
        int totalEnemies = waveNumber * 5;

        for (int i = 0; i < totalEnemies; i++)
        {
            float enemyPrefabToSpawn = Random.Range(0f, 1f);
            if (enemyPrefabToSpawn > 0.3f)
            {
                Vector3 posToSpawn = new Vector3(Random.Range(0f, 14.5f), 11, 0);
                GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
            }
            else
            {
                Vector3 posToSpawn = new Vector3(-16.7f, Random.Range(2.0f, 6.5f), 0);
                GameObject newEnemy = Instantiate(_enemy2Prefab, posToSpawn, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
            }

            if ((i + 1) % enemiesPerGroup == 0)
            {
                yield return new WaitForSeconds(3f);
            }
        }

        while (GameObject.FindWithTag("Enemy") != null)
        {
            yield return null;
        }
    }

    */
    //*****************************************************************************************
    /*
        IEnumerator EnemyWaveTwoRoutine(int enemies, float delayToSpawn)
        {
            Debug.Log("wave2 initiated");
            yield return new WaitForSeconds(3.0f);
            while (enemies > 0 && _stopSpawning == false)
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
                enemies--;
                yield return new WaitForSeconds(3.0f);
            }
            StartCoroutine(EnemyWaveThreeRoutine(5, 2.5f));
        }

        IEnumerator EnemyWaveThreeRoutine(int enemies, float delayToSpawn)    
        {
            Debug.Log("wave3 initiated");
            yield return new WaitForSeconds(3.0f);
            while (enemies > 0 && _stopSpawning == false)
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
                enemies--;
                yield return new WaitForSeconds(3.0f);
            }
            StartCoroutine(EnemyWaveFourRoutine(5, 2.0f));
        }

        IEnumerator EnemyWaveFourRoutine(int enemies, float delayToSpawn)
        {
            Debug.Log("wave4 initiated");
            yield return new WaitForSeconds(3.0f);
            while (enemies > 0 && _stopSpawning == false)
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
                enemies--;
                yield return new WaitForSeconds(3.0f);
            }
            StartCoroutine(EnemyWaveFiveRoutine(30, 1.5f));
        }

        IEnumerator EnemyWaveFiveRoutine(int enemies, float delayToSpawn)
        {
            Debug.Log("wave5 initiated");
            yield return new WaitForSeconds(3.0f);
            while (enemies > 0 && _stopSpawning == false)
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
                enemies--;
                yield return new WaitForSeconds(3.0f);
            }
        }
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
