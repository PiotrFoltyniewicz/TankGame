using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : Bullet
{

    void OnDestroy()
    {
        tankScript.bulletsLeft++;
    }
}
