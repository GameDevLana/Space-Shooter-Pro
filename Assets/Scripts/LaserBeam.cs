using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private bool _isLaserBeamActive = false;

    void Start()
    {
        
    }


/*  Create laserbeam using raycast and physics2d and linerenderer (or sprite) to be used by an enemytype yet tbd.
 *  It will instantiate at a given speed or time interval that may or may not be random. An initial time delay may
 *  be appropriate. Consider direction laser will need to go in relation to the enemy and at the point it should start.
 *  it should turn off when it collides with with the player and calls Damage. Will the enemy laser beam continue
 *  to instantiate at intervals after an initial contact with player or should it be neutralized once it hits the
 *  player. What about other objects such as powerups. Will it hit powerups and what will be the consequence.
*/
    void Update()
    {
        
    }
}
