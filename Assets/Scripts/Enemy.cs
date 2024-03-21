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

   
    
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
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


    /*will enemies actually have the methods and coroutines like player and if so it will be a
    mixture of similarity to powerup script and player script.*/
    //each enemy wave will hold a unique enemy type

    //OPTIONS  Enemy One - will alternate straight and diagonal
    //OPTIONS  Enemy Two - will fire and damage player
    //OPTIONS  Enemy Four - aggressive
    //OPTIONS  Enemy Five - smart

    //Enemy shields
    //one enemy with shields
    //one enemy avoids lasers
    //one enemy pickups?

    //Boss AI final wave- stops at center of scene with unique attack which could be a combo of all enemey types
    // all the enemies types


    void CalculateMovement()

/*
    Enemy One - normal movemenT*/        /* Enemy One commented out-testing new movement "enemy two" a new movement for same enemy.
                                          * May/May not be assigned to 2nd enemy. Will check decision to create new enemy sprites or shapes for prototyping & time purposes.*/
    /*  
        {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    } 
    */

//  Enemy Two - diagonal movement following
{
        transform.Translate(new Vector3(1, -3, 0).normalized * _speed * Time.deltaTime);
    

        if (transform.position.y< -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
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
            
//  OPTIONS enemy damage method like player(shields)
            
            Destroy(this.gameObject, 2.5f);
        }
       
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

           if (_player != null)
           {
                _player.AddScore(10);
           }

//  OPTIONS enemy damage method like player(shields)

            _anim.SetTrigger("OnEnemyDeath");

//  explosion animation ^^^^

            _speed = 0;

            _audioSource.Play();
            
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
        }

    }

}