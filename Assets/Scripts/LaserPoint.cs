using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPoint : MonoBehaviour
{
    public float bulletSpeed;
    public float numberOfBounces;
    public float bulletLifetime;
    protected Rigidbody2D rb;
    public Tank tankScript;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed * Time.fixedDeltaTime;
    }
    private void Update()
    {
        bulletLifetime -= Time.deltaTime;
        if (bulletLifetime < 0)
        {
            Destroy(gameObject);
        }
    }
}
