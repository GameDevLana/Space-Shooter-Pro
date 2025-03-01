using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImg;
    
    [SerializeField]
    private Sprite[] _liveSprites;
   
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private Slider _thrustGauge;

    private GameManager _gameManager;



    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _ammoText.text = "Ammo Count: " + 15;

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL.");
        }
    }


    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        if (currentLives >= 0)
        {
            _livesImg.sprite = _liveSprites[currentLives];
        }
        else
        {
            _livesImg.sprite = _liveSprites[0];
        }
        
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    public void UpdateAmmo(int currentAmmo)
    { 
        _ammoText.text = "Ammo Count: " + currentAmmo.ToString();
    }

    public void UpdateThrusterCharge(float value)
    {
        _thrustGauge.value = value;
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }


    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    } 
}