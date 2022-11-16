using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Tank : MonoBehaviour
{
    InputAction shootAction;
    public GameObject defaultBullet;
    GameObject choosenBullet;
    Transform firePoint;
    public int bulletsLeft;
    int specialBulletsAmount;
    string currentBullet;
    public bool isBuffed;
    public GameObject deathEffect;

    public GameObject shieldObject;
    GameObject shield;
    public float shieldTime;
    float shieldTimeLeft;
    bool setInputs = false;

    void Awake()
    {
        firePoint = transform.GetChild(0);
        shootAction = GetComponent<PlayerInput>().currentActionMap.FindAction("Shoot");
        choosenBullet = defaultBullet;
        bulletsLeft = 4;
        isBuffed = false;
    }

    void Update()
    {
        if (GameLoop.started && !setInputs)
        {
            shootAction.performed += _ => Shoot();
            setInputs = true;
        }
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
            if(currentBullet == "Shotgun")
            {
                Shotgun();
                if (specialBulletsAmount <= 0)
                {
                    choosenBullet = defaultBullet;
                    isBuffed = false;
                }
                return;
            }
            GameObject bullet = Instantiate(choosenBullet, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().tankScript = this;
            if (specialBulletsAmount <= 0)
            {
                choosenBullet = defaultBullet;
                isBuffed = false;
            }
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
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    public void GetBuff(string name, GameObject bulletType = null, int bulletsAmount = 0)
    {
        if(name == "Shield")
        {
            if (shield is null) shield = Instantiate(shieldObject, transform.position, transform.rotation);
            shieldTimeLeft = shieldTime;
        }
        else
        {
            choosenBullet = bulletType;
            specialBulletsAmount = bulletsAmount;
            currentBullet = name;
            isBuffed = true;
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            GotHit();
            Destroy(collision.gameObject);
        }
    }
    */
}
