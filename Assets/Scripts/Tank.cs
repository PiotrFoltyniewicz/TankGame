using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tank : MonoBehaviour
{
    InputAction shootAction;
    public GameObject defaultBullet;
    GameObject choosenBullet;
    Transform firePoint;
    public int bulletsLeft;
    int specialBulletsAmount;

    public GameObject shieldObject;
    GameObject shield;
    public float shieldTime;
    float shieldTimeLeft;

    void Awake()
    {
        firePoint = transform.GetChild(1);
        shootAction = GetComponent<PlayerInput>().currentActionMap.FindAction("Shoot");

        shootAction.performed += _ => Shoot();
        choosenBullet = defaultBullet;
        bulletsLeft = 5;
    }

    void Update()
    {
        if (shield is not null)
        {
            shieldTimeLeft -= Time.deltaTime;
            shield.transform.position = transform.position;

            if (shieldTimeLeft < 0 && shield.activeInHierarchy)
            {
                Destroy(shield);
                shield = null;
            }
        }
    }

    void Shoot()
    {
        if (specialBulletsAmount > 0 && choosenBullet != defaultBullet)
        {
            specialBulletsAmount--;
        }
        else
        {
            choosenBullet = defaultBullet;
        }
        if (choosenBullet == defaultBullet) bulletsLeft--;
        if (choosenBullet == defaultBullet && bulletsLeft <= 0) return;
        GameObject bullet = Instantiate(choosenBullet, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().tankScript = this;
        
    }

    public void GotHit()
    {
        Destroy(gameObject);
    }

    public void GetBuff(string name, GameObject bulletType = null, int bulletsAmount = 0)
    {
        if(name == "Shield")
        {
            shield = Instantiate(shieldObject, transform.position, transform.rotation);
            shieldTimeLeft = shieldTime;
        }
        else
        {
            choosenBullet = bulletType;
            specialBulletsAmount = bulletsAmount;
        }
    }

}
