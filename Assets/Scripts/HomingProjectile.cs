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
    private float _projectileSpeed = 10.0f;

   // private bool _isEnemyLaser = false;


    private void Update()
    {
        FindClosestEnemy();
        TrackEnemy();
        MoveUp();
    }

    void MoveUp()
    {
         transform.Translate(_projectileSpeed * Time.deltaTime * Vector3.up);

         if (transform.position.y > 8f)             //****ADD OUT OF BOUNDS FOR LOWER AND BOTH SIDES. ****
        {
          Destroy(this.gameObject);
         }
    }

    private void FindClosestEnemy()
    {
        targets = GameObject.FindGameObjectsWithTag("Enemy");

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
        if (_target != null)
        {
            Vector3 dir = _target.position - transform.position;
            dir.Normalize();

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle-90f, Vector3.forward);

            //*********ADD FORWARD MOVEMENT IN ADDITION TO THE MOVEUP METHOD????******
        }
       
    }
}
