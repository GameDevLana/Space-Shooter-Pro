using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;

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
    private float _checkRadius = 1.0f;

    //[SerializeField]
    // private float _rotationModifier = 0;
    //[SerializeField]
    //private float _turnSpeed = 1.0f;
    //private bool _turnStarted = false;


    // private float _distanceToLaser;
    //private float _defenseRange = 10.0f;
    //private Vector3 _avoidDirection;
    //  [SerializeField]
    // private bool _canAvoidShot;
    //private bool _avoidShot;

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
        //  CheckforLasers(); ****TRACK PLAYER LASERS FOR AVOID BEHAVIOR***
        // AvoidLaser();   ****TRACK PLAYER LASERS FOR AVOID BEHAVIOR***
    }
    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (_player != null && Vector3.Distance(gameObject.transform.position, _player.transform.position) <= _checkRadius)
        
        {
            Vector3 directionAway = (Laser.transform.position - transform.position).normalized;
            float angle = 30f;                                             // Small angle to shift slightly away from laser
            Quaternion rotation = Quaternion.Euler(0, 0, angle);           // Rotate by 10 degrees for a slight dodge
                                                                          // Rotate the direction vector slightly
            Vector3 adjustedDirection = rotation * directionAway;
                                                                          // Move just enough to get out of the laser's path
            float dodgeSpeed = 3f;                                        // Lower speed for a small movement
            
            transform.position += adjustedDirection * dodgeSpeed * Time.deltaTime;
        }
        
        if (transform.position.y < -5f || transform.position.x < -8f || transform.position.x > 8f)
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
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.5f);
        }
        Laser laser = other.GetComponent<Laser>();  // **Retrieve Laser component**
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
            _anim.SetTrigger("OnEnemyDeath");   // explosion animation 
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
        }
    }
}
    //**************************************
    //FAILED ATTEMPTS AT AVOID BEHAVIOR
    //****************************************
    /*  void EnemyAvoidShot()
      {
          Vector3 newXPos = new Vector3(x[Index], 0, 0);
          Vector3 newPos = transform.position += newXPos;
          transform.position = Vector3.MoveTowards(transform.position, newPos, _speed * Time.deltaTime);
          _avoidShot = false;
          _canAvoidShot = false;
      }*/

    /* void CheckforLasers()
     {
         Collider2D[] other = Physics2D.OverlapCircleAll(transform.position, _checkRadius);
         foreach (var hitObject in other)
         {
             if (hitObject.CompareTag("Laser"))
             { 
                 Laser shot = hitObject.GetComponent<Laser>();
                 if(shot != null && !_turnStarted)
                 {
                     StartCoroutine(AvoidShot(hitObject.transform.position));
                 }
             }    
         }
     }/*
     /*  void AvoidLaser()
       {
           GameObject _laser = GameObject.Find("Laser(Clone)");
           if(_laser != null)
           {
               _distanceToLaser = Vector3.Distance(_laser.transform.position, this.transform.position);
               if (_distanceToLaser <= _defenseRange *2)
               {
                   _avoidDirection = this.transform.position - _laser.transform.position;
                   _avoidDirection = _avoidDirection.normalized;
                   this.transform.position += _avoidDirection * Time.deltaTime * (_speed * 5);
               }
           }
       }*/
    //public void EnemyDamage()   consider Damage method of enemy shield to take one hit instead of onTrigger
    /*I  void TurnShip(Vector3 target)
      {
          Vector3 vectorToTarget = (target * -1) - transform.position;
          float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - _rotationModifier;
          Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
          transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _turnSpeed);
      }*/

    /* IEnumerator AvoidShot(Vector3 target)
     {
         TurnShip(target);
         yield return new WaitForSeconds(0.75f);
         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 90), Time.deltaTime * _turnSpeed);
         _turnStarted = true;
         yield return new WaitForSeconds(0.5f);
         _turnStarted = false;
     }*/



