using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tank : MonoBehaviour
{
    InputAction shootAction;
    GameObject defaultBullet;
    public GameObject[] bullets;

    void Awake()
    {
        shootAction = GetComponent<PlayerInput>().currentActionMap.FindAction("Shoot");

        shootAction.performed += _ => Shoot();
        defaultBullet = bullets[0];
    }

    void Shoot()
    {

    }

}
