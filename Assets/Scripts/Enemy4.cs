using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private GameObject _laserPrefab;

    private Player _player;

    private Animator _anim;

    private AudioSource _audioSource;
    private float _canFireBack = -1;
    private float _fireRate = 3.0f;
    private float _canFire = -1;
    private float _backFireRate = 0.5f;

    private void Start()
    {
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
        CheckPlayerPosition();

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

    void CalculateMovement() //Enemy moving down
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }
   
    private void CheckPlayerPosition() //Enemy moving up
    {
        if (_player != null)
        {
            if ((int)_player.transform.position.y > (int)transform.position.y) 
            {
                if (Time.time > _canFireBack)
                {
                    _canFireBack = Time.time + _backFireRate; 
                    GameObject enemyLaser = Instantiate(_laserPrefab, transform.position + new Vector3(0, 2.74f, 0), Quaternion.identity);
                    if (enemyLaser == null)
                    {
                        Debug.LogError("Enemy laser was not instantiated correctly");
                    }
                    if (_player == null)
                    {
                        Debug.LogError("Player reference is null");
                    }    
                    Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
                    for (int i = 0; i < lasers.Length; i++)
                    {
                        lasers[i].AssignEnemyLaserUp();
                    }
                }
            }
        }
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
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.5f);
        }
        Laser laser = other.GetComponent<Laser>();  // **Retrieve Laser component**
        if (laser != null && !laser.IsEnemyLaser() && !laser.IsEnemyLaserUp())
        {
            Destroy(other.gameObject);
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
    }
}





