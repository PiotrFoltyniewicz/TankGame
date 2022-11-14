using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
//using UnityEngine.AI;

public class MapGeneration : MonoBehaviour
{
    public class Tile
    {
        public bool visited;
        public bool[] walls = new bool[4]; // 0 -> north, 1 -> east, 2 -> south, 3 -> west
        public bool isBorder;
    }
    public GameObject mapTile;
    GameObject[,] instantiatedMap;
    public List<Transform> placeableTiles = new List<Transform>();
    Tile[,] map;
    public Camera cam;
    int height;
    int width;

   //NavMeshSurface2d navMesh;

    float time;
    void Awake()
    {
        //navMesh = GetComponent<NavMeshSurface2d>();
        GenerateMap();
        instantiatedMap = InstantiateMap();
        DisableRandomWalls();
        //navMesh.BuildNavMeshAsync();
    }

    void GenerateMap()
    {
        height = Random.Range(4, 6);
        width = Random.Range(height, 9);
        map = new Tile[height + 2, width + 2];
        CreateBorders();
        CreateInside();
        CreateRandomHoles();


        cam.transform.position = new Vector3(width, -height - 0.5f, -10);
        cam.orthographicSize = height + 1;
        
        (int, int) currentTile;
        
        while (true)
        {
            currentTile = (Random.Range(1, height + 1), Random.Range(1, width + 1));
            if (!map[currentTile.Item1, currentTile.Item2].isBorder) break;
        }
        Stack<(int, int)> path = new Stack<(int, int)>();

        int k = 0;
        while(k < 1000)
        {
            k++;
            map[currentTile.Item1, currentTile.Item2].visited = true;

            List<(int, int)> neighbours = CheckNeighbours(currentTile);

            if(neighbours.Count == 0)
            {
                if (path.Count == 0) break;
                else currentTile = path.Pop();
            }
            else
            {
                path.Push(currentTile);
                (int, int) newTile = neighbours[Random.Range(0, neighbours.Count)];

                //north
                if(newTile.Item1 + 1 == currentTile.Item1)
                {
                    map[currentTile.Item1, currentTile.Item2].walls[0] = true;
                    currentTile = newTile;
                    map[currentTile.Item1, currentTile.Item2].walls[2] = true;
                }
                //east
                else if (newTile.Item2 - 1 == currentTile.Item2)
                {
                    map[currentTile.Item1, currentTile.Item2].walls[1] = true;
                    currentTile = newTile;
                    map[currentTile.Item1, currentTile.Item2].walls[3] = true;
                }
                //south
                else if (newTile.Item1 - 1 == currentTile.Item1)
                {
                    map[currentTile.Item1, currentTile.Item2].walls[2] = true;
                    currentTile = newTile;
                    map[currentTile.Item1, currentTile.Item2].walls[0] = true;
                }
                //west
                else if (newTile.Item2 + 1 == currentTile.Item2)
                {
                    map[currentTile.Item1, currentTile.Item2].walls[3] = true;
                    currentTile = newTile;
                    map[currentTile.Item1, currentTile.Item2].walls[1] = true;
                }
            }
        }
        RemoveBlockedTiles();
    }

    void CreateRandomHoles()
    {
        for (int i = 1; i < height + 1; i++)
        {
            for (int j = 1; j < width + 1; j++)
            {
                if (Random.Range(0, 15) == 0) map[i, j].isBorder = true;
            }
        }
    }

    void RemoveBlockedTiles()
    {
        for(int i = 1; i < height + 1; i++)
        {
            for(int j = 1; j < width + 1; j++)
            {
                if (!map[i, j].visited) map[i, j].isBorder = true;
            }
        }
    }

    GameObject[,] InstantiateMap()
    {
        GameObject[,] instMap = new GameObject[height, width];
        for (int i = 1; i < height + 1; i++)
        {
            for (int j = 1; j < width + 1; j++)
            {
                if (map[i, j].isBorder)
                {
                    instMap[i - 1, j - 1] = null;
                    continue;
                }
                GameObject tile = Instantiate(mapTile, new Vector2(j * 2 - 1, -i * 2 + 1), Quaternion.identity, transform);
                instMap[i - 1, j - 1] = tile;
                placeableTiles.Add(tile.transform);
                if (Random.Range(0, 2) == 0) tile.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f);
                for (int k = 0; k < 4; k++)
                {
                    if (map[i, j].walls[k]) tile.transform.GetChild(k).gameObject.SetActive(false);
                }
            }
        }
        return instMap;
    }
    void DisableRandomWalls()
    {
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                if (instantiatedMap[i, j] is null) continue;
                //north
                if(Random.Range(0, 2) == 0 && !map[i, j + 1].isBorder)
                {
                    instantiatedMap[i, j].transform.GetChild(0).gameObject.SetActive(false);
                }
                //east
                if (Random.Range(0, 2) == 0 && !map[i + 1, j + 2].isBorder)
                {
                    instantiatedMap[i, j].transform.GetChild(1).gameObject.SetActive(false);
                }
                //south
                if (Random.Range(0, 2) == 0 && !map[i + 2, j + 1].isBorder)
                {
                    instantiatedMap[i, j].transform.GetChild(2).gameObject.SetActive(false);
                }
                //west
                if (Random.Range(0, 2) == 0 && !map[i + 1, j].isBorder)
                {
                    instantiatedMap[i, j].transform.GetChild(3).gameObject.SetActive(false);
                }
            }
        }
    }

    List<(int,int)> CheckNeighbours((int,int) tile)
    {
        List<(int, int)> neighbours = new List<(int, int)>();

        //north
        if (!map[tile.Item1 - 1, tile.Item2].isBorder && !map[tile.Item1 - 1, tile.Item2].visited) neighbours.Add((tile.Item1 - 1, tile.Item2));
        //east
        if (!map[tile.Item1, tile.Item2 + 1].isBorder && !map[tile.Item1, tile.Item2 + 1].visited) neighbours.Add((tile.Item1, tile.Item2 + 1));
        //south
        if (!map[tile.Item1 + 1, tile.Item2].isBorder && !map[tile.Item1 + 1, tile.Item2].visited) neighbours.Add((tile.Item1 + 1, tile.Item2));
        //west
        if (!map[tile.Item1, tile.Item2 - 1].isBorder && !map[tile.Item1, tile.Item2 - 1].visited) neighbours.Add((tile.Item1, tile.Item2 - 1));

        return neighbours;
    }
    void PrintMap()
    {
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < map.GetLength(0); i++)
        {
            for(int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] is null) sb.Append("Null"); 
                else sb.Append(map[i, j].isBorder);
                sb.Append('\t');
            }
            sb.AppendLine();
        }
        Debug.Log(sb.ToString());
    }

    void CreateInside()
    {
        for(int i = 1; i < height + 1; i++)
        {
            for(int j = 1; j < width + 1; j++)
            {
                map[i, j] = new Tile();
            }
        }
    }

    void CreateBorders()
    {
        for (int i = 0; i < height + 2; i++)
        {
            if (map[i, 0] is not null) continue;
            map[i, 0] = new Tile();
            map[i, 0].isBorder = true;
        }
        for (int i = 0; i < height + 2; i++)
        {
            if (map[i, width + 1] is not null) continue;
            map[i, width + 1] = new Tile();
            map[i, width + 1].isBorder = true;
        }
        for (int i = 0; i < width + 2; i++)
        {
            if (map[0, i] is not null) continue;
            map[0, i] = new Tile();
            map[0, i].isBorder = true;
        }
        for (int i = 0; i < width + 2; i++)
        {
            if (map[height + 1, i] is not null) continue;
            map[height + 1, i] = new Tile();
            map[height + 1, i].isBorder = true;
        }
    }
}
