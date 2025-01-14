using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //[SerializeField]
    //private float _speed = 4f;

    [SerializeField]
    private GameObject _laserPrefab;

    private Player _player;

    private Animator _anim;

    private AudioSource _audioSource;




    void Start()
    {
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

        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("The Animator is NULL");
        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("The Audio is NULL");
        }

    }

  //  void Update()
    //{
      //  transform.parent = null;
   // }
}
