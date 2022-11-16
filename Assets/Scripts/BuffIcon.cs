using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffIcon : MonoBehaviour
{
    public Sprite[] icons;
    Transform tank;
    SpriteRenderer spriteRend;


    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.sprite = icons[0];
        Invoke("GetTank", 0.5f);
    }

    void Update()
    {
        transform.position = new Vector2(tank.GetComponent<Rigidbody2D>().position.x, tank.GetComponent<Rigidbody2D>().position.y + 0.5f);
    }
    public void ShowIcon(string buffName)
    {
        switch (buffName)
        {
            case "Double":
                spriteRend.sprite = icons[1];
                break;
            case "Shotgun":
                spriteRend.sprite = icons[2];
                break;
            case "Laser":
                spriteRend.sprite = icons[3];
                break;
            case "Rocket":
                spriteRend.sprite = icons[4];
                break;
        }

    }

    void GetTank()
    {
        if(gameObject.name == "GreenBuffIcon")
        {
            tank = GameObject.Find("Tank1(Clone)").transform;
        }
        else
        {
            tank = GameObject.Find("Tank2(Clone)").transform;
        }
    }

    public void HideIcon()
    {
            spriteRend.sprite = icons[0];
    }
}
