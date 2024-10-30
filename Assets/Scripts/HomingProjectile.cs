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
    [SerializeField]
    private float _projectileSpeed = 100.0f;
    [SerializeField]
    private float _projectileTurnSpeed = 20f;

   // private bool _isEnemyLaser = false;

    void Start()
    {

        _projectile = GetComponent<Rigidbody2D>();
        if (_projectile == null)
        {
            Debug.LogError("The Rigidbody is NULL!");
        }
       
    }

    private void FixedUpdate()
    {
        FindClosestEnemy();
        TrackEnemy();
    }

    void MoveUp()
    {
         transform.Translate(_projectileSpeed * Time.deltaTime * Vector3.up);
         if (transform.position.y > 8f)
         {
          Destroy(this.gameObject);
         }
    }

    private void FindClosestEnemy()
    {
        targets = GameObject.FindGameObjectsWithTag("Enemy");
       
        if (targets.Length == 0)
        {
            MoveUp();
        }

        if (_target == null)
        {

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
    }

    private void TrackEnemy()
    {
        _projectile.velocity = _projectileSpeed * Time.fixedDeltaTime * transform.up;
        if (_target != null)
        {
            Vector2 direction = (Vector2)_target.position - _projectile.position;
            direction.Normalize();

            float rotationValue = Vector3.Cross(direction, transform.up).z;
            _projectile.angularVelocity = -rotationValue * _projectileTurnSpeed;
            _projectile.velocity = transform.up * _projectileSpeed * Time.fixedDeltaTime;
        }
       
    }
}
