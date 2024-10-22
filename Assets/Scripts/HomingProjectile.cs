using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    private Transform _target = null;
    private GameObject[] targets;

    [SerializeField]
    private Rigidbody2D _rbMissile;
    private float _distance;
    private float _closestTarget = Mathf.Infinity;
    private float _missileSpeed;
    private float _missileTurnSpeed;

   // private bool _isEnemyLaser = false;


    void Start()
    {

        _rbMissile = GetComponent<Rigidbody2D>();
        if (_rbMissile == null)
        {
            Debug.LogError("The Rigidbody is NULL!");
        }
        FindClosestEnemy();
    }

    private void FixedUpdate()
    {
        FireMissile();
    }

    void Update()
    {

    }

    private void FindClosestEnemy()
    {
        targets = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in targets)
        {
            _distance = (enemy.transform.position - this.transform.position).sqrMagnitude;
            if (_distance < _closestTarget)
            {
                _closestTarget = _distance;
                _target = enemy.transform;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")  // && (_isEnemyLaser == true || _isEnemyLaserUp == true))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
        }
    }

    private void FireMissile()
    {
        _rbMissile.velocity = transform.up * _missileSpeed * Time.deltaTime;
        if (_target != null)
        {
            Vector2 direction = (Vector2)_target.position - _rbMissile.position;
            direction.Normalize();

            float rotationValue = Vector3.Cross(direction, transform.up).z;
            _rbMissile.angularVelocity = -rotationValue * _missileTurnSpeed;
            _rbMissile.velocity = transform.up * _missileSpeed * Time.deltaTime;
        }
    }
   









}
