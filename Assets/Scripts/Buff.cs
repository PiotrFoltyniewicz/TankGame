using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public string buffName;
    public GameObject bulletType;
    public int bulletAmount;
    PlacingObjects placeObj;

    private void Awake()
    {
        placeObj = GameObject.Find("GameManagement").GetComponent<PlacingObjects>();
        placeObj.placedBuffs++;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Tank"))
        {
            if (collision.GetComponent<Tank>().isBuffed && buffName != "Shield") return;
            collision.gameObject.GetComponent<Tank>().GetBuff(buffName, bulletType, bulletAmount);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        placeObj.placedBuffs--;
    }
}
