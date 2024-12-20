using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;

    private bool _isEnemyLaser = false;
    private bool _isEnemyLaserUp = false;
    public bool IsEnemyLaser()  
    {
        return _isEnemyLaser;  
    }
    public bool IsEnemyLaserUp()  
    {
        return _isEnemyLaserUp;  
    }

        void Update()
    {
        if (_isEnemyLaser == false || _isEnemyLaserUp == true)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }
                //**Laser enums maybe. Set direction methods/behavior for all.**
    void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
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
            //**assign pulse laser that causes damage**
    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }
    public void AssignEnemyLaserUp()
    {
        _isEnemyLaserUp = true;    
    }
            //***ontrigger with enemy shield? identify shield and destroy laser **
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && (_isEnemyLaser == true || _isEnemyLaserUp == true))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
        }
    }
}
            