using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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
    public GameObject _enemyShieldPrefab;
    public bool _enemyShieldActive = false;

    // enemy shield visualizer set to false

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

//method for adding shield - turn shield on
//turn on visualizer - set to true 
//when to call method - need to be called in spawnmgr
//behavior of shield - it will take 1 hit damage  in Ontrigger & use shield sprite from player 
    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    public void EnemyShieldActivated()
    {
        _enemyShieldActive = true;
        _enemyShieldPrefab.SetActive(true);
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
                _enemyShieldPrefab.SetActive(false);
                _enemyShieldActive = false;
                return;
            }
            
                  //  turn shield off
                  //else destroy this object

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();


            Destroy(this.gameObject, 2.5f);
        }
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            //if shield is active 
            //turn shield off

            //else destroy this object

            if (_player != null)
            {
                _player.AddScore(10);
            }
            _anim.SetTrigger("OnEnemyDeath");   // explosion animation 
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);

            //the shield visualizer destroys with this object at bottom of screen - sets visualizer back to false

        }

        //public void EnemyDamage ()       Damage method of enemy shield to take one hit


        _player?.AddScore(10);
    }

}


