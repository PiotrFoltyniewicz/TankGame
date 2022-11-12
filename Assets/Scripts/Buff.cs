using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public string buffName;
    public GameObject bulletType;
    public int bulletAmount;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Tank"))
        {
            collision.gameObject.GetComponent<Tank>().GetBuff(buffName, bulletType, bulletAmount);
            Destroy(gameObject);
        }
        
    }
}
