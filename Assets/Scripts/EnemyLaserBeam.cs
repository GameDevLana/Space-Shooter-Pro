using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserBeam : MonoBehaviour
{

    [SerializeField]
    private float _speed = 4.5f;


    private Player _player;

    [SerializeField]
    private GameObject _explosionPrefab;

    // private Animator _anim;

    //private AudioSource _audioSource;

    // private float _fireRate = 3.0f;
    // private float _canFire = -1;


    /* 1. add variables for extra things to make particular to this enemy later
     *      a. anim 
     *      b. audio 
     *      
       2. will laserbeam be a prefab or something else like raycast or line renderer

       3. will I be using _fireRate and _canfire for this enemy*/


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        // _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
        //_anim = GetComponent<Animator>();

        //if (_anim == null)
        //{
        //   Debug.LogError("The Animator is NULL");
        // }

    }

    //enemy needs to be instantiated at a random location and have defined
    //movement include the game window boundary limits. The spawn manager controls
    //instantiation of enemies in waves with coroutines. The enemy will cause damage to
    //the player upon collision. Enemy must be able to communicate with the player.
    //The enemy will be destroyed when hit by player laser or player.
    //Recycle enemies not destroyed. The trait of this enemy will be a laserbeam, so I
    //need to create the laserbeam. Animations and audio for enemy explosions and
    //laserbeams. Similar to first enemy, destroy collider and then destroy enemy to 
    //prevent double hits.  


    //********need to figure out how to create the laserbeam.**********



    // Update is called once per frame
    void Update()
    //add enemy movement
    {

        CalculateMovement();

    }

    void CalculateMovement()

    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
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

            // _anim.SetTrigger("OnEnemyDeath");
            //  explosion animation ^^^^

            _speed = 0;

            //_audioSource.Play();

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 2.5f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(10);
            }

            // _anim.SetTrigger("OnEnemyDeath");
            //  explosion animation ^^^^

            _speed = 0;

            // _audioSource.Play();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
        }
    }
}