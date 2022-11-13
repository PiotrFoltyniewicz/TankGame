using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBullet : Bullet
{
    private Transform target;
    private Transform[] tanks = new Transform[2];
    public float distanceToFollow;
    public float angleChangingSpeed;
    public float timeToActivate;
    private float timePassed;

    private void Start()
    {
        tanks[0] = GameObject.Find("Tank1").transform;
        tanks[1] = GameObject.Find("Tank2").transform;
    }
    void FixedUpdate()
    {
        timePassed += Time.deltaTime;
        timeToActivate -= Time.fixedDeltaTime;
        if (timeToActivate < 0)
        {
            target = GetTarget();
            if (Vector2.Distance(transform.position, target.position) < distanceToFollow) FollowTarget();
        }
    }

    Transform GetTarget()
    {
        if (tanks[0] is null) return tanks[1];
        if (tanks[1] is null) return tanks[0];
        float distance1 = Vector2.Distance(transform.position, tanks[0].position);
        float distance2 = Vector2.Distance(transform.position, tanks[1].position);
        if (distance1 < distance2) return tanks[0];
        else return tanks[1];
    }

    void FollowTarget()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.right).z;
        rb.angularVelocity = -angleChangingSpeed * rotateAmount;
        rb.rotation += Mathf.Sin(timePassed * 8) * 4;
        rb.velocity = transform.right * bulletSpeed * Time.fixedDeltaTime;
    }
}
