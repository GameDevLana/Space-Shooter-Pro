using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    private Animator _anim;

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

   /* [SerializeField]
    private GameObject _enemyFleetOne;
    [SerializeField]
    private GameObject _enemyFleetThree;
    [SerializeField]
    private GameObject _enemyFleetFour;
    [SerializeField]
    private GameObject _enemyFleetFive;
    [SerializeField]
    private GameObject _enemyFleetSix;
    [SerializeField]
    private GameObject _enemyFleetSeven;
    [SerializeField]
    private GameObject _enemyFleetEight;
    [SerializeField]
    private GameObject _enemyFleetNine;
    [SerializeField]
    private GameObject _enemyFleetTen;
   */
    [SerializeField]
    private GameObject[] enemyFleet;
    [SerializeField]
    private GameObject[] enemyPrefabs;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    public int _totalEnemyFleet;

    [SerializeField]
    private GameObject _bossPrefab;
    [SerializeField]
    private GameObject _station;

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

    //[SerializeField]
    //public int _totalEnemyFleet;


    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());    //***Randomized instantiation for enemysheildprefab to be childed to ALL enemy types?***
        StartCoroutine(SpawnPowerupRoutine());  //***Consider adding a mystery powerup that can be really special or negative = randomly.***

    }

    public void StartSpaceStation()
    {
        StartCoroutine(SpawnSpaceStation());
    }


    public void EnemyFleetLaunch()            //****Instantiate fleetwaves of miniships from SpaceStation.*****
    {
    StartCoroutine(SpawnEnemyFleetRoutine()); 
    }

    


//                                          ********************Spawn MiniFleets**************


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

            }

        /*
      ////////////////////////////////////////////////////////////////////
                              
                                       //**OPTIONAL: Add a short delay between enemy spawns in the same wave:****

        yield return new WaitForSeconds(0.5f);

        // ***Wait for all enemies to be destroyed before starting the next wave:*****

        while (GameObject.FindWithTag("Enemy") != null)
        {
            yield return null;
        }
        
        /////////////////////////////////////////////////////////////// */



        /*
         IEnumerator SpawnBossRoutine()
          {
            yield return new WaitForSeconds(2.0);
            while (_stopBoss == false)
            {
                Vector3 bossSpawnPos = new(-11, 13, 0);
                Instantiate(_bossPrefab, bossSpawnPos, Quaternion.identity);
                _stopBoss = true;
            }
          } 

            /*       

      ///////////////////////////////////////////////////////////////
      
                                        //****OPTIONAL: Add a delay between waves:*******

            yield return new WaitForSeconds(5f); // Wait 5 seconds between waves
            Debug.Log("All waves completed!");

      *//////////////////////////////////////////////////////

    }
    IEnumerator SpawnPowerupRoutine()
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

    IEnumerator SpawnEnemyFleetRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopSpawning == false)
        {
            int totalEnemyFleet = _totalEnemyFleet;
            for (int i = 0; i < totalEnemyFleet; i++)
            {
                Vector3 posToSpawn = new Vector3(-9, 4, 0);
                Instantiate(enemyFleet[i % enemyFleet.Length], posToSpawn, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(1, 3));
            }
        }
    }


    IEnumerator SpawnSpaceStation()
    {
        yield return new WaitForSeconds(2.0f);
        Vector3 posToSpawn = new Vector3(0, 4.5f, 0);
        Instantiate(_station, posToSpawn, Quaternion.identity);
        _anim.SetTrigger("Space_Station_Enter_anim 1");
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}