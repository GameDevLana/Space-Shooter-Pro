using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    
    [SerializeField]
    private GameObject _enemyContainer;
    
    [SerializeField]    
    private GameObject[] powerups;
    private bool _stopSpawning = false;

    public List<GameObject> powers;

    public int[] table =
    {
        19,
        19,
        19,
        19,
        19,
        5,
    };

    public int total;
    public int randomNumber;
   
    void Start()
    {
       
    }


    public void StartSpawning(int num, float delay) 
    {
        StartCoroutine(SpawnEnemyRoutine(num, delay));
       //possible multiple coroutines 

      //IEnumerator WaveTwoRoutine()
      /*{
            yield return new WaitForSeconds(2.0f);
            while (_stopSpawning == false)
            {
                Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                int randomPowerUp = Random.Range(0, 6);
                Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(3, 8));
            }
         }

      //IEnumerator WaveThreeRoutine()
        {
            yield return new WaitForSeconds(2.0f);
            while (_stopSpawning == false)
            {
                Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                int randomPowerUp = Random.Range(0, 6);
                Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(3, 8));
            }
         }

       //IEnumerator WaveFourRoutine()
         {
             yield return new WaitForSeconds(2.0f);
             while (_stopSpawning == false)
             {
                 Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                 int randomPowerUp = Random.Range(0, 6);
                 Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
                 yield return new WaitForSeconds(Random.Range(3, 8));
             }
         }


        //IEnumerator WaveFiveRoutine()
          {
              yield return new WaitForSeconds(2.0f);
              while (_stopSpawning == false)
              {
                  Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                  int randomPowerUp = Random.Range(0, 6);
                  Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
                  yield return new WaitForSeconds(Random.Range(3, 8));
          }
    }  */


        //yield return StartCoroutine (WaveOneRoutine());

        //yield return StartCoroutine (WaveTwoRoutine());

        //yield return StartCoroutine (WaveThreeRoutine());

        //yield return StartCoroutine (WaveFourRoutine());

        //yield return StartCoroutine (WaveFiveRoutine());       






        IEnumerator SpawnEnemyRoutine(int enemies, float delayToSpawn) //(int enemies, float delaytospawn)
        //IEnumerator WaveOneRoutine()


        {
            yield return new WaitForSeconds(2.0f);
            while (enemies > 0 && !_stopSpawning == false)
            {
                Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(delayToSpawn);
            }
        }


        //spawn enemies - 10 with delay between each spawn
        //wait
        //spawn enemies - 15 with delay between each spawn
        //wait
        //spawn enemies - 20 with delay between each spawn
        //wait
        //spawn enemies - 25 with delay between each spawn
        //wait
        //spawn enemies - infinite with delay between each spawn. CURRENT SPAWN BEHAVIOR 
        //stop spawning at player death


        








    
        IEnumerator SpawnPowerupRoutine()
        {
            yield return new WaitForSeconds(2.0f);
            while (_stopSpawning == false)
            {
                Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                int randomPowerUp = Random.Range(0, 6);
                Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(3, 8));
            }
        }


    } 

    public void OnPlayerDeath()
{ 
    _stopSpawning = true;
}
    }
