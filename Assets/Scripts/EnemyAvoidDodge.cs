using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAvoidDodge : MonoBehaviour
{
    private Enemy5 _enemy;

    void Start()
    {
        {
            _enemy = GetComponentInParent<Enemy5>();

            if (_enemy == null)
            {
                Debug.LogError("Enemy5 script not found on parent GameObject!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            Laser laser = other.GetComponent<Laser>();
            if (laser != null && !laser.IsEnemyLaser() && !laser.IsEnemyLaserUp())
            {
                Debug.Log("Laser detected by child object!");
                _enemy.Dodge();
            }
        }
    }
}



