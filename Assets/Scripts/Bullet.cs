using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float numberOfBounces;
    public float bulletLifetime;
    protected Rigidbody2D rb;
    public Tank tankScript;
    AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        gameObject.layer = 12;
        Invoke("ActivateCollider", 0.05f);
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

    void ActivateCollider()
    {
        gameObject.layer = 7;
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Tank"))
        {
            collision.gameObject.GetComponent<Tank>().GotHit();
            Destroy(gameObject);
            return;
        }
        audio.Play();
    }
}
