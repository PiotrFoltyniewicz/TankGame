using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankMovement : MonoBehaviour
{
    Rigidbody2D rb;
    InputAction moveAction;
    InputAction rotateAction;

    public float moveSpeed;
    public float rotateSpeed;

    bool isMoving;
    bool isRotating;
    float movement;
    float rotation;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = GetComponent<PlayerInput>().currentActionMap.FindAction("Movement");
        rotateAction = GetComponent<PlayerInput>().currentActionMap.FindAction("Rotation");


        moveAction.performed += ctx => movement = ctx.ReadValue<float>() * moveSpeed * Time.fixedDeltaTime;
        moveAction.canceled += ctx => movement = 0;

        rotateAction.performed += ctx =>
        {
            rotation = ctx.ReadValue<float>() * rotateSpeed * Time.fixedDeltaTime;
            isRotating = true;
        };
        rotateAction.canceled += ctx =>
        {
            rotation = 0;
            isRotating = false;
        };
    }

    void FixedUpdate()
    {
        if (isRotating) rb.rotation -= rotation;
        else rb.angularVelocity = 0;
        rb.MovePosition(transform.position + (transform.right * movement));
    }
}

