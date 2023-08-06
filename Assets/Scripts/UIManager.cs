using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //handle to text
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
    //handle to text
    [SerializeField]
    private Text _ammoText;

    private GameManager _gameManager;
    //create a handle to text (score)
    //create a handle to text (ammo)

    void Start()
        //assign text component to the handle (score)
        //assign text component to the handle (ammo)
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

    //method to update score 
    //method to update ammo
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    public void UpdateAmmo(int currentAmmo)
    { 
        _ammoText.text = "Ammo Count: " + currentAmmo.ToString();
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