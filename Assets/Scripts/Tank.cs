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

        shield = transform.GetChild(2).gameObject;
        //shield.SetActive(false);
    }

    void Update()
    {
        shieldTimeLeft -= Time.deltaTime;
        //if (shieldTimeLeft < 0 && shield.activeInHierarchy) shield.SetActive(false);
    }

    void Shoot()
    {   if (choosenBullet == defaultBullet && bulletsLeft <= 0) return;
        GameObject bullet = Instantiate(choosenBullet, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().tankScript = this;
        if (choosenBullet == defaultBullet) bulletsLeft--;
        else choosenBullet = defaultBullet;
    }

    public void GotHit()
    {
        Destroy(gameObject);
    }

    public void GetBuff(string name, GameObject bulletType = null, int bulletsAmount = 0)
    {
        if(name == "Shield")
        {
            shield.SetActive(true);
            shieldTimeLeft = shieldTime;
        }
        else if(name == "Double")
        {
            bulletsLeft = 10;
        }
        else
        {
            choosenBullet = bulletType;
        }
    }

}
