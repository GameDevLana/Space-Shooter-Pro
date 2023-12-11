using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private float _boost = 8.5f;

    //variable for thruster max/min time
    //variable for current thruster time

    [SerializeField]
    private float _thrust = 1.5f;
    [SerializeField]
    private float _maxThrust = 100;
    [SerializeField]
    private float _currentThrust;


    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canfire = -1f;


    [SerializeField]
    private int _maxAmmo = 15;
    private int _currentAmmo;


    [SerializeField]
    private int _lives = 3;


    private SpawnManager _spawnManager;

    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;
    private bool _isStopFireActive = false;
     
    private bool _isMisfireActive = false;

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
    [SerializeField]
    private AudioClip _explodeSoundClip;
    private AudioSource _audioSource;

    private CameraManager _cameraHolder;


    //private no ammo audio - click when press space bar                                                                                                                                                                                    

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        _sprite = _shieldVisualizer.GetComponent<SpriteRenderer>();
        _cameraHolder = GameObject.Find("Main Camera").GetComponent<CameraManager>();

        _currentAmmo = _maxAmmo;

        _currentThrust = _maxThrust;


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
        if (_cameraHolder == null)
        {
            Debug.LogError("The CameraManager is NULL");
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

        if (Input.GetKey(KeyCode.LeftShift) && _currentThrust > 0)
        {
            transform.Translate(Vector3.right * horizontalInput * _speed * _thrust * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * _speed * _thrust * Time.deltaTime);
            StopCoroutine(ThrustRechargeRoutine());
            ThrusterActive();
        }
        
        else if (_currentThrust <=0)
        {

            StartCoroutine(ThrustRechargeRoutine());   
        }


        //*Adding extra UI for Charge (*opt UI level indicator could include text of %) 


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

        if (_isTripleShotActive == true && _currentAmmo > 2)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            _audioSource.Play();

            //UI - NO AMMO - false
            //ammo --3 for triple shot
        }
        


        else if (_currentAmmo > 0 && _isStopFireActive == false)
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
            _audioSource.Play();
            _currentAmmo--;
            _uiManager.UpdateAmmo(_currentAmmo);
        }

    }

    public void Damage()
    {
        _cameraHolder.ShakeCamera();

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

        _lives--;


        //call camera shake coroutines
        //create a handle to the camera


        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }
        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            AudioSource.PlayClipAtPoint(_explodeSoundClip, transform.position);
            Destroy(this.gameObject);
        }
        _uiManager.UpdateLives(_lives);
    }


    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
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

    public void AmmoActive()
    {
        _currentAmmo = _maxAmmo;
        _uiManager.UpdateAmmo(_currentAmmo);
    }
    /* ADDING EXTRA UI FOR AMMO
     if (_maxAmmo < 4)
    UI flash current ammo count
    UI play warning alarm
    if (_maxAmmo <= 0)
    play empty ammo noise
    UI - NO AMMO = true
    */

    public void HealthActive()
    {
        if (_lives < 3)
        {
            _lives++;
        }

        if (_lives == 2)
        {
            _rightEngine.SetActive(false);
        }

        else if (_lives == 3)
        {
            _leftEngine.SetActive(false);
        }
        _uiManager.UpdateLives(_lives);
    }

    public void BombActive()
    {
        {
            _isStopFireActive = true;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            _audioSource.Play();
            Destroy(enemy);
        }

        StartCoroutine(StopFireCooldownRoutine());
    }


    public void ThrusterActive()
    {
        if (_currentThrust > 0)
        {
            _currentThrust -= _speed * _thrust * Time.deltaTime;
            _uiManager.UpdateThrusterCharge(_currentThrust);
            
        }

    }

  /*public void MisfireActive()
    {
        _isMisfireActive = true;
        StartCoroutine(MisfireCooldownRoutine());
    }
  */


    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(6.0f); 
        _isTripleShotActive = false;
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
    }

    IEnumerator StopFireCooldownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isStopFireActive = false;
    }

    IEnumerator ThrustRechargeRoutine()
    {
        while (_currentThrust !=100)
        {
            {
                yield return new WaitForSeconds(5.0f);
                _currentThrust += _thrust * Time.deltaTime;
                _uiManager.UpdateThrusterCharge(_currentThrust);
            }
            if (_currentThrust >= 100)
            {
                _currentThrust = 100;
                _uiManager.UpdateThrusterCharge(_currentThrust);

                break;
            }
        }
    }

    /*IEnumerator MisfireCooldownRoutine()
     {
        yield return new WaitForSeconds(5.0f);
        _isMisfireActive = false;
     }
    */
        //flash current ammo count when it become <5
       //flash NO AMMO when current ammo = 0

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    
}
    

