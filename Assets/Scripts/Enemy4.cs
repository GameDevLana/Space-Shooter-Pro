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

    private float _fireRate = 3.0f;
    private float _canFire = -1;


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

    void CalculateMovement() //ENEMY MOVING DOWN
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }
   
    private void CheckPlayerPosition()
    {
        if (_player != null)
        {
            if ((int)_player.transform.position.y > (int)transform.position.y)
            {
                Debug.Log("Player is behind enemy.");
                        //TAKING AWAY RANDOM FIRERATE
                        //_fireRate = Random.Range(.5f, 1.0f);
                yield return new WaitForSeconds(1.0f);
                {
                            // Instantiate the laser at the enemy's position with a small offset
                    GameObject enemyLaser = Instantiate(_laserPrefab, transform.localPosition + new Vector3(0, 1.47f, 0), Quaternion.identity);
                            //INSTANTIATE LASER BACKFIRE
                  
                    _laserPrefab.GetComponent<Laser>().AssignEnemyLaserUp();
                    Debug.Log("Enemy Laser Backwards is Active");

                            // Call the method to handle the backward laser logic
                            // Get all laser components attached to the enemy laser and assign them
                    Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
                    _canFire = Time.time;    // Update the fire time

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
        if (other.tag == "Laser")
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
        _player?.AddScore(10);
    }
}





