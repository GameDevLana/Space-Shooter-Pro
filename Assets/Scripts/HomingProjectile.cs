using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    private Transform _target = null;
    private GameObject[] targets;

    [SerializeField]
    private Rigidbody2D _projectile;
    private float _distance;
    private float _closestTarget = Mathf.Infinity;
    private float _projectileSpeed;
    private float _projectileTurnSpeed;

   // private bool _isEnemyLaser = false;

    void Start()
    {

        _projectile = GetComponent<Rigidbody2D>();
        if (_projectile == null)
        {
            Debug.LogError("The Rigidbody is NULL!");
        }
     //   FindClosestEnemy();                      //NEEED TO ADD GENERIC MOVEMENT UP
    }

    void Update()
    {
        MoveUp();    
    }
    private void FixedUpdate()
    {
        TrackEnemy();
    }

    void MoveUp()
    {
         transform.Translate(_projectileSpeed * Time.deltaTime * Vector3.up);
         if (transform.position.y > 8f)
         {
          Destroy(this.gameObject);
         }
        Debug.Log("projectile will move up now");
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
        Debug.Log("Finding closest enemy in scene.");
    }

    private void TrackEnemy()
    {
        _projectile.velocity = _projectileSpeed * Time.deltaTime * transform.up;
        if (_target != null)
        {
            Vector2 direction = (Vector2)_target.position - _projectile.position;
            direction.Normalize();

            float rotationValue = Vector3.Cross(direction, transform.up).z;
            _projectile.angularVelocity = -rotationValue * _projectileTurnSpeed;
            _projectile.velocity = transform.up * _projectileSpeed * Time.deltaTime;
        }
    }

}
