using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private float _boost = 8.5f;

    [SerializeField]
    private float _thrust = 1.5f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canfire = -1f;


    [SerializeField]
    private int _maxAmmo = 15;
    //private int _currentAmmo;


    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private int _spriteRenderer = 3;
    private SpriteRenderer _sprite;
    [SerializeField]
    private Color _shieldColorOne;
    [SerializeField]
    private Color _shieldColorTwo;


    [SerializeField]
    private GameObject _rightEngine, _leftEngine;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;
    //private no ammo click when press space bar 


    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        _sprite = _shieldVisualizer.GetComponent<SpriteRenderer>();
        

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on the player is NULL");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }


    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)
        {
            FireLaser();
        }
    }


    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (_isSpeedBoostActive == true)
        {
            transform.Translate(Vector3.right * horizontalInput * _boost * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * _boost * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.right * horizontalInput * _speed * _thrust * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * _speed * _thrust * Time.deltaTime);
        }

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }


    void FireLaser()
    {
        _canfire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            _audioSource.Play();
            //UI - NO AMMO - false
        }
        
        else if (_maxAmmo > 0)
        {
          //  _ = _maxAmmo > 0;
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
            _audioSource.Play();
            _maxAmmo--;
            _uiManager.UpdateAmmo(_maxAmmo);
        }
        
        // if (_maxAmmo < 4)
        // {
        //UI flash current ammo count
        //UI play warning alarm
        //}
        //if (_maxAmmo <= 0)
        // {
        //play empty ammo noise
        //UI - NO AMMO = true
        //}

    }


    public void Damage()
    {

        if (_isShieldActive == true)
        {
            _spriteRenderer--;
        
            if (_spriteRenderer == 2)
            {
                _sprite.color = _shieldColorOne;
            }
            else if (_spriteRenderer == 1)
            {
                _sprite.color = _shieldColorTwo;
            }


            if (_spriteRenderer < 1)
            {
                _isShieldActive = false;
                _shieldVisualizer.SetActive(false);
                _sprite.color = Color.white;
                _spriteRenderer = 3;
            }
            
            return;
        }
  
        _lives --;

        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
                else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }
        _uiManager.UpdateLives(_lives);

        if (_lives <1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }  
    
    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
       // StartCoroutine(ShieldPowerDownRoutine());  
    }   
    

    public void SpeedBoostActive() 
    {
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }


    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    
   // IEnumerator ShieldPowerDownRoutine()
    //{
        //yield return new WaitForSeconds(15.0f);
       //_isShieldActive = false;
    //}

    //method to add 10 to the score
    //communicate with the UI to update score

    //method to decremate ammo by one
    //communicate with the UI to update amm
    
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    IEnumerator SpeedBoostPowerDownRoutine() 
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;    
    }

    //flash current ammo count when it become <5
    //flash NO AMMO when current ammo = 0
    public void AddScore(int points) 
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
