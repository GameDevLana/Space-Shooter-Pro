using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField] //0 = triple shot 1 = speed 2 = shield 3 = ammo 4 = health 5 = bomb  6 = misfire  
    private int powerupID;

    [SerializeField]
    private AudioClip _clip;

    private float _range = 3f;
    private Player _player;


    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        
    }
   
    //**Homing projectile powerup - rare**
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }

        if(Input.GetKey(KeyCode.C) && Vector3.Distance(_player.transform.position, transform.position) < _range)
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
        }
    }
    //Add HomingProjectilePowerup. Will it be added to this switch statement/array or do I make a new script for enemy powerups? Attach new PU sprite in Inspector. 
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    case 3:
                        player.AmmoActive();
                        break;
                    case 4:
                        player.HealthActive();
                        break;
                    case 5:
                        player.BombActive();
                        break;
                    case 6:
                      player.MisfireActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
                Destroy(this.gameObject);
                return;
            }
        }
        if (other.tag == "EnemyLaser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}


