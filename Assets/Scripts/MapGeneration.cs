using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public class Tile
    {
        public bool visited;
        public bool[] walls = new bool[4];
        public bool isBorder;
    }
    public GameObject mapTile;
    public Tile[,] map;
    public Camera cam;
    int height;
    int width;

    private float timer;
    void Awake()
    {
        GenerateMap();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                }
            }
            timer = 2f;
            GenerateMap();

        }
    }

    void GenerateMap()
    {
        height = Random.Range(4, 6);
        width = Random.Range(height, 9);
        map = new Tile[height + 1, width + 1];

        CreateBorders();
    }

    void CreateBorders()
    {
        for (int i = 0; i < height; i++)
        {
            if (map[i, 0] is not null) continue;
            map[i, 0] = new Tile();
            map[i, 0].isBorder = true;
        }
        for (int i = 0; i < height; i++)
        {
            if (map[i, width - 1] is not null) continue;
            map[i, width - 1] = new Tile();
            map[i, width - 1].isBorder = true;
        }
        for (int i = 0; i < width; i++)
        {
            if (map[0, i] is not null) continue;
            map[0, i] = new Tile();
            map[0, i].isBorder = true;
        }
        for (int i = 0; i < width; i++)
        {
            if (map[0, height - 1] is not null) continue;
            map[0, height - 1] = new Tile();
            map[0, height - 1].isBorder = true;
        }
    }

    bool CheckAround(int j, int i)
    {
        try
        {
            if (map[i - 1, j - 1] is null) return false;
            else if (map[i - 1, j] is null) return false;
            else if (map[i - 1, j + 1] is null) return false;
            else if (map[i, j + 1] is null) return false;
            else if (map[i + 1, j + 1] is null) return false;
            else if (map[i + 1, j] is null) return false;
            else if (map[i + 1, j - 1] is null) return false;
            else if (map[i, j - 1] is null) return false;
        }
        catch
        {
            return true;
        }
        return true;
    }
}
