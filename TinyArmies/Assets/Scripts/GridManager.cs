using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private AstarPath astarPath;
    [SerializeField] public int width, height;

    [SerializeField] private GameObject tilePrefab;

    [SerializeField] private GameObject tree;

    [SerializeField] private GameObject rock;

    public List<List<GameObject>> tiles = new List<List<GameObject>>();

    public int shoots;

    void Awake()
    {
        if(Instance == null)
            Instance=this;
        else
            Destroy(gameObject);

        GenerateGrid();

        GenerateObstical(50, tree);
        GenerateObstical(50, rock);
    }

    void GenerateGrid()
    {
        for(int x = 0; x < width; x++)
        {
            tiles.Add(new List<GameObject>());

            for(int y = 0; y < height; y++)
            {
                var newTile = Instantiate(tilePrefab, new Vector3(x, y), transform.rotation);
                tiles[x].Add(newTile);
                
                newTile.transform.SetParent(gameObject.transform);
            }
        }
    }

    void GenerateObstical(int spawns, GameObject item)
    {
        for(int z = 0; z < spawns; z++)
        {
            bool foundSpot = false;
            for(int i = 0; i < shoots; i++)
            {
                if(!foundSpot)
                {
                    var x = Random.Range(0, width);
                    var y = Random.Range(0, height);

                    if(!tiles[x][y].GetComponent<TileScript>().occupied)
                    {
                        GameObject newItem = Instantiate(item, new Vector3(x, y), transform.rotation);
                        tiles[x][y].GetComponent<TileScript>().occupied = true;
                        foundSpot = true;

                        newItem.transform.SetParent(gameObject.transform);
                    }
                }
            }
        }

        Rescan();
    }

    void GenerateBigObstical(int spawns, GameObject item)
    {
        for(int z = 0; z < spawns; z++)
        {
            bool foundSpot = false;
            for(int i = 0; i < shoots; i++)
            {
                bool tilesUnoccupied = true;

                if(!foundSpot)
                {
                    Vector2 size = item.transform.localScale;

                    int x = Random.Range(0, (int)(width - size.x));
                    int y = Random.Range(0, (int)(height - size.y));

                    for(int a = 0; a < size.x; a++)
                    {
                        for(int b = 0; b < size.y; b++)
                        {
                            if(tiles[x + a][y + b].GetComponent<TileScript>().occupied)
                            {
                                tilesUnoccupied = false;
                            }
                        }
                    }

                    if(tilesUnoccupied)
                    {
                        GameObject newItem = Instantiate(item, new Vector3(x + (size.x - 1)/2, y + (size.y - 1)/2), transform.rotation);

                        for(int a = 0; a < size.x; a++)
                        {
                            for(int b = 0; b < size.y; b++)
                            tiles[x + a][y + b].GetComponent<TileScript>().occupied = true;
                        }

                        foundSpot = true;

                        newItem.transform.SetParent(gameObject.transform);
                    }
                }
            }
        }

        Rescan();
    }

    void Rescan()
    {
        astarPath.Scan();
    }
}
