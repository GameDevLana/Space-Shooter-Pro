using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserBeam : MonoBehaviour
{

    [SerializeField]
    private float _speed = 4f;

    //[SerializeField]
    //private GameObject _laserPrefab;

    private Player _player;

    //private Animator _anim;

    private AudioSource _audioSource;

   // private float _fireRate = 3.0f;
   // private float _canFire = -1;








    // Start is called before the first frame update
    void Start()
    {
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
            _audioSource = GetComponent<AudioSource>();

            if (_player == null)
            {
                Debug.LogError("The Player is NULL.");
            }
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
