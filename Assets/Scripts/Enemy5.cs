using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    private Player _player;

    [SerializeField]
    private GameObject _explosionPrefab;
    private AudioSource _audioSource;

    // private float _fireRate = 3.0f;
    //  private float _canFire = -1;

    // [SerializeField]              ************SHIELDS********
    // public GameObject _enemyShield;
    //public bool _enemyShieldActive = false;
    // [SerializeField]
    // private float _percentEnemyShield = 0.2f;
    ////////////////////////////////////////////////////////////

    [SerializeField]
    private int _dodgeSpeed = 6;
    private bool _isDodgeOn = false;

    //[SerializeField]                      **********DODGECOUNT*******
    // private int _randomNumber = Random.Range(0, 2);
    // [SerializeField]
    // private _dodgeLaserCount = 0;
    //////////////////////////////////////////////////////////////

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
        if (_audioSource == null)
        {
            Debug.LogError("The Audio is NULL");
        }

    }

    void Update()
    {

        CalculateMovement();
       /* if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
                                                        //*************************ENEMY LASERS OFFSET SHOULD BE CLOSER TO SHIP AND SMALLER/SHORTER************
        }*/
    }

    void CalculateMovement()
    {
        if (_isDodgeOn == true)
        {
            transform.Translate(_dodgeSpeed * Time.deltaTime * Vector3.right);
        }
        else
        { 
            transform.Translate(_speed * Time.deltaTime * Vector3.down);
        }

        if (transform.position.y < -5f) //also if it goes out of bounds horizontally.
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }


    /*  public void EnemyShieldActivated()  ************SHIELDS********
      {
          _enemyShieldActive = true;
          _enemyShield.SetActive(true);
      }
      public void EnemyShieldDeactivated()
      {
          _enemyShieldActive = false;
          _enemyShield.SetActive(false);
      }*////////////////////////////////////////////////////////////////
    //                                 *************************************** ENEMY HAS NO WEAPON- DOESN'T FIRE ANYTHING***************
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)

            {
                player.Damage();
            }
            /*  if (_enemyShieldActive == true)        *********SHIELDS*******
              {
                  EnemyShieldDeactivated();
                  return;
              }*/////////////////////////////////////////////////////////////////
        }

        Laser laser = other.GetComponent<Laser>();

        if (laser != null && !laser.IsEnemyLaser() && !laser.IsEnemyLaserUp())
        {
            Destroy(other.gameObject);
            /* if (_enemyShieldActive == true)     ************SHIELDS********
             {
                 _enemyShield.SetActive(false);
                 _enemyShieldActive = false;
                 return;
             }*///////////////////////////////////////////////////////////////
            if (_player != null)  //Player scores for hitting enemy with laser
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
        Destroy(this.gameObject, .5f);
    }

    public void Dodge()
    {
        {
            Debug.Log("Dodging laser.");
            _isDodgeOn = true;
            StartCoroutine(DodgeDelayRoutine());
        }
    }
    IEnumerator DodgeDelayRoutine()
    {
        yield return new WaitForSeconds(.5f);
        _isDodgeOn = false;
    }

    //transform.Translate(Vector3.right * _dodgeSpeed * _direction * Time.deltaTime);
    //transform.Translate(Vector3.right *5); //simple example


    //public void EnemyDamage()   consider Damage method of enemy shield to take one hit instead of onTrigger

}




