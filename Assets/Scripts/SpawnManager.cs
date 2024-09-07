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
            yield return new WaitForSeconds(1.0f); // StartCoroutine(EnemyWaveTwoRoutine(15, 3f)); // float parameter here is the delaytospawn between instantiating EACH enemy - not the wave itself"
        }
    }
    /*

    IEnumerator EnemyWaveTwoRoutine(int enemies, float delayToSpawn)    
    {
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
       //   yield return StartCoroutine(EnemyWaveThreeRoutine(20, 2.5f));
            }
        }

    IEnumerator EnemyWaveThreeRoutine(int enemies, float delayToSpawn)    
    {
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
            yield return StartCoroutine(EnemyWaveFourRoutine(25, 2.0f));
            }
        }

    IEnumerator EnemyWaveFourRoutine(int enemies, float delayToSpawn)
    {
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
            yield return StartCoroutine(EnemyWaveFiveRoutine(30, 1.5f));
        }
    }

    IEnumerator EnemyWaveFiveRoutine(int enemies, float delayToSpawn)
    {
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
            yield return StartCoroutine(EnemyWaveRoutineOver());
        }
    IEnumerator EnemyWaveRoutineOver()
    {
        _stopSpawning = true;
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
