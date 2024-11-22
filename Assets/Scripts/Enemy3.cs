using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    [SerializeField]
    private GameObject _laserPrefab;

    private Player _player;

    private Animator _anim;

    private AudioSource _audioSource;

    private float _fireRate = 3.0f;
    private float _canFire = -1;

    [SerializeField]
    public GameObject _enemyShield;
    public bool _enemyShieldActive = false;          
    [SerializeField]
    private float _percentEnemyShield = 0.2f;
    [SerializeField]
    private float _ramRange = 4f;

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
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("The Animator is NULL");
        }
    }


    void Update()
    {
        CalculateMovement();
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
    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (_player != null && Vector3.Distance(gameObject.transform.position, _player.transform.position) <= _ramRange)
        {
            Vector3 directionVector = (_player.transform.position - transform.position).normalized;
            transform.Translate(directionVector * _speed * Time.deltaTime);
        }
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

        Laser laser = other.GetComponent<Laser>();  

        if (laser != null && !laser.IsEnemyLaser() && !laser.IsEnemyLaserUp())
        {
            Destroy(other.gameObject);

            if (_enemyShieldActive == true)
            {
                _enemyShield.SetActive(false);
                _enemyShieldActive = false;
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
        _anim.SetTrigger("OnEnemyDeath");
        _audioSource.Play();
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject, 2.5f);
    }


    //public void EnemyDamage()   consider Damage method of enemy shield to take one hit instead of onTrigger
}


