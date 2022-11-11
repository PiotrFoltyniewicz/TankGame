using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float numberOfBounces;
    public float bulletLifetime;
    protected Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed * Time.fixedDeltaTime;
    }
    private void Update()
    {
        bulletLifetime -= Time.deltaTime;
        if(bulletLifetime < 0)
        {
            Destroy(gameObject);
        }
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Tank"))
        {
            collision.gameObject.GetComponent<Tank>().GotHit();
            Destroy(gameObject);
            return;
        }
        /*
        if(collision.transform.CompareTag("Wall") && numberOfBounces > 0)
        {
            numberOfBounces--;
            return;
        }
        */
    }
}
