using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : Bullet
{
    void Start()
    {
        bulletLifetime -= Random.Range(0, 0.2f);
    }

}
