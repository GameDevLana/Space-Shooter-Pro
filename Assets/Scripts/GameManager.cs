using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    /*            ************************ENEMY WAVE SYSTEM - IS THIS VALID OR DO I DELETE?**************
     * [SerializeField]
    private int _maxWaves;
    private int _currentWave;

    [SerializeField]
    private int _enemiesPerWave;
    *//////////////////////////////////////////////////

    private UIManager _uiManaager;
    private SpawnManager _spawnManager;
       
    [SerializeField]
    private bool _isGameOver;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(0); //Current Game Scene
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

 /*                *****************ENEMY WAVE SYSTEM - IS THIS VALID OR DO I DELETE?*********
  * public void DestroyEnemies()
    {
        _currentEnemies--;
      
       if(_currentEnemies == 0 && _currentWave < _maxWaves)
       {
            StartCoroutine(NewWave());
       }
    }
 */
 /*
     * public IEnumerator NewWave()
    {
        _currentWave++;
        _currentEnemies = _enemiesPerWave * _currentWave;

        yield return new WaitForSeconds(2f);

        float delayOfSpawn = 2f / (float)_currentWave;
        _spawnManager.StartSpawning(_currentEnemies, delayOfSpawn);
    }
 *//////////////////////////////////////////////////////////////////////////////

    public void GameOver()
    {
        _isGameOver = true;
    }
}