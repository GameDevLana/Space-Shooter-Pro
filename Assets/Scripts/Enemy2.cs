using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.5f;

    [SerializeField]
    private GameObject _laserPrefab;

    private Player _player;

    [SerializeField]
    private GameObject _explosionPrefab;
    private AudioSource _audioSource;

    private float _fireRate = 3.0f;
    private float _canFire = -1;

    [SerializeField]
    private float _percentEnemyShield = 0.2f;
    [SerializeField]
    public GameObject _enemyShield;
    public bool _enemyShieldActive = false;       


    private void Start()
    {
        if (Random.Range(0f, 1f) < _percentEnemyShield)
        {
            EnemyShieldActivated();
        }
        else
        {
            EnemyShieldDeactivated();
        }
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _player = player.GetComponent<Player>();
            if (_player == null)
            {
                Debug.LogError("The Player is NULL.");
            }
        }
        else
        {
            Debug.LogError("Player Object is not found.");
        }
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("The Audio is NULL");
        }

    
    }


    void Update()
    {
        DiagonalMovement();
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }
    void DiagonalMovement()  //Enemy Two - diagonal movement
    {
        transform.Translate(new Vector3(1, -3, 0).normalized * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }
    public void EnemyShieldActivated()
    {
        _enemyShieldActive = true;
        _enemyShield.SetActive(true);
    }
    public void EnemyShieldDeactivated()
    {
        _enemyShieldActive = false;
        _enemyShield.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            
            if (player != null)
            {
                player.Damage();
            }
            
            if (_enemyShieldActive == true)
            {
                EnemyShieldDeactivated();
                return;
            }
        }

                                                        //********** ADJUST LASER TO SHOOT DIAGONALLY**************
        
        Laser laser = other.GetComponent<Laser>();  // **Retrieve Laser component**
        
        if (laser != null && !laser.IsEnemyLaser() && !laser.IsEnemyLaserUp())
        {
            Destroy(other.gameObject);

            if (_enemyShieldActive == true)
            {
                EnemyShieldDeactivated();
                return;
            }
            
            if (_player != null)
            {
                _player.AddScore(10);
            }
        }

        HomingProjectile projectile = other.GetComponent<HomingProjectile>();

        if (projectile != null)
        {
            Destroy(other.gameObject);
            /* if (_enemyShieldActive == true)     ************SHIELDS********
             {
                 _enemyShield.SetActive(false);
                 _enemyShieldActive = false;
                 return;
             }*///////////////////////////////////////////////////////////////
            if (_player != null)   //Player scores for hitting enemy with homing projectile
            {
                _player.AddScore(10);
            }
        }

        EnemyDeath();
    }

    private void EnemyDeath()
    {
        _speed = 0;
        _audioSource.Play();
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject, 2.5f);
    }

    //public void EnemyDamage()   consider Damage method of enemy shield to take one hit instead of onTrigger
}
