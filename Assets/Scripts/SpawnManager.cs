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

        StartCoroutine(SpawnPowerupRoutine());//consider adding a mystery powerup that can be really special or negative = randomly
    }


   // yield return StartCoroutine(EnemyWaveTwoRoutine(15, 3f));
     //   yield return StartCoroutine(EnemyWaveThreeRoutine(20, 3f));
       // yield return StartCoroutine(EnemyWaveFourRoutine(25, 3f));
        // yield return StartCoroutine(EnemyWaveFiveRoutine(30, 3f));


    IEnumerator SpawnEnemyRoutine(int enemies, float delayToSpawn) 
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopSpawning == false)
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

            yield return new WaitForSeconds(4.0f);
        }
    }
      
    

     
    IEnumerator EnemyWaveTwoRoutine(int enemies, float delayToSpawn)    
    {
        yield return new WaitForSeconds(2.0f);
        while (enemies < 0 && _stopSpawning == false)
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
            yield return new WaitForSeconds(delayToSpawn);
        }
    }
     
    IEnumerator EnemyWaveThreeRoutine(int enemies, float delayToSpawn)    
    {
        yield return new WaitForSeconds(2.0f);
        while (enemies < 0 && _stopSpawning == false)
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
            yield return new WaitForSeconds(delayToSpawn);
        }
    }
    
    IEnumerator EnemyWaveFourRoutine(int enemies, float delayToSpawn)
    {
        yield return new WaitForSeconds(2.0f);
        while (enemies < 0 && _stopSpawning == false)
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
            yield return new WaitForSeconds(delayToSpawn);
        }
    }
   

   
    IEnumerator EnemyWaveFiveRoutine(int enemies, float delayToSpawn)
    {
        yield return new WaitForSeconds(2.0f);
        while (enemies < 0 && _stopSpawning == false)
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
            yield return new WaitForSeconds(delayToSpawn);
        }
    }
    

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

    /*
        use powerups.Length instead of Random.Range(0, 70);
        for (int i = 0; i < powerups.Length; i++)  
            {
                int randomPowerUp = powerups[i];
            }
    */
    public void OnPlayerDeath()
    { 
    _stopSpawning = true;
    }
}
