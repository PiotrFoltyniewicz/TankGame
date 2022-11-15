using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingObjects : MonoBehaviour
{
    MapGeneration mapGen;
    public int placedBuffs;
    public GameObject[] tanks;
    public GameObject[] buffsObj;
    Transform[] placedTanks = new Transform[2];
    public float buffTime;
    float buffTimeLeft;
    void Awake()
    {
        mapGen = GetComponent<MapGeneration>();
        buffTimeLeft = buffTime;
    }

    void Update()
    {
        if (!GameLoop.started) return;
        buffTimeLeft -= Time.deltaTime;
        if(buffTimeLeft < 0)
        {
            PlaceBuff();
            buffTimeLeft = buffTime;
        }
    }

    public Transform[] PlaceTanks()
    {
        Transform tankTile1 = mapGen.placeableTiles[Random.Range(0, mapGen.placeableTiles.Count)];
        placedTanks[0] = Instantiate(tanks[0], tankTile1.position, Quaternion.Euler(0, 0, Random.Range(0, 360))).transform;

        Transform tankTile2;
        int k = 0;
        do
        {
            tankTile2 = mapGen.placeableTiles[Random.Range(0, mapGen.placeableTiles.Count)];
            k++;
        }
        while (Vector2.Distance(tankTile1.position, tankTile2.position) < 6 && k < 1000);

        placedTanks[1] = Instantiate(tanks[1], tankTile2.position, Quaternion.Euler(0, 0, Random.Range(0, 360))).transform;
        return placedTanks;
    }

    void PlaceBuff()
    {
        if (placedBuffs >= 3)
        {
            buffTimeLeft = buffTime;
            return;
        }
        Transform buffTile;
        int k = 0;
        do
        {
            buffTile = mapGen.placeableTiles[Random.Range(0, mapGen.placeableTiles.Count)];
            k++;
        } 
        while (Vector2.Distance(placedTanks[0].position, buffTile.position) < 3 && Vector2.Distance(placedTanks[1].position, buffTile.position) < 3 && k < 1000);
        Instantiate(buffsObj[Random.Range(0, buffsObj.Length)], new Vector2(buffTile.position.x + Random.Range(-0.5f,0.5f), buffTile.position.y + Random.Range(-0.5f, 0.5f)), Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }
}
