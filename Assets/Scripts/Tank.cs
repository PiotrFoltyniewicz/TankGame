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
    string currentBullet;

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
        bulletsLeft = 4;
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
            if (specialBulletsAmount <= 0) choosenBullet = defaultBullet;
            specialBulletsAmount--;
            if(currentBullet == "Shotgun")
            {
                Shotgun();
                return;
            }
            GameObject bullet = Instantiate(choosenBullet, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().tankScript = this;
            return;
        }
        else if (bulletsLeft > 0)
        {
            currentBullet = "Basic";
            choosenBullet = defaultBullet;
            bulletsLeft--;
            GameObject bullet = Instantiate(choosenBullet, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().tankScript = this;
        }
    }

    void Shotgun()
    {
        for(int i = 0; i < 15; i++)
        {
            float angle = Random.Range(-20, 21);
            GameObject bullet = Instantiate(choosenBullet, firePoint.position, Quaternion.Euler(firePoint.eulerAngles.x, firePoint.eulerAngles.y, firePoint.eulerAngles.z + angle));
            bullet.GetComponent<Bullet>().tankScript = this;
        }
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
            currentBullet = name;
        }
    }

}
