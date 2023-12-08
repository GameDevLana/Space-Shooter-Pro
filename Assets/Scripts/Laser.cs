using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    private bool _isEnemyLaser = false;


    void Update()
    {     
        if (_isEnemyLaser == false)
        {
            MoveUP();
        }
        else
        {
            MoveDown();
        }
    }


    void MoveUP()
    {

      /*if (_isMisfireActive = true)
        {
            transform.Translate (new Vector3(.5, 4, 0).normalized * _speed * Time.deltaTime); //Weapon Misfire PU - Causes the player's weapons to misfire randomly, reducing accuracy 
            transform.Translate (new Vector3(1, -3, 0).normalized * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        */




        if (transform.position.y > 8f)
        {
            
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }


    void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8f)
        {
            
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }


    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
        }
    }
}
