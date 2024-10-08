using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : MonoBehaviour
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



    void Start()
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
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.5f);
        }
        if (other.tag == "Laser")
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
            _anim.SetTrigger("OnEnemyDeath");   // explosion animation 
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
        }
        _player?.AddScore(10);
    }
}
