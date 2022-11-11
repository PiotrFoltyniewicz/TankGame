using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tank : MonoBehaviour
{
    InputAction shootAction;
    GameObject choosenBullet;
    public GameObject[] bullets;
    Transform firePoint;

    void Awake()
    {
        firePoint = transform.GetChild(1);
        shootAction = GetComponent<PlayerInput>().currentActionMap.FindAction("Shoot");

        shootAction.performed += _ => Shoot();
        choosenBullet = bullets[2];
    }

    void Shoot()
    {
        Instantiate(choosenBullet, firePoint.position, firePoint.rotation);
    }

    public void GotHit()
    {
        Destroy(gameObject);
    }

}
