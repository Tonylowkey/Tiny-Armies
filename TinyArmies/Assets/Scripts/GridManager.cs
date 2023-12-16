using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;

    [SerializeField] private GameObject tilePrefab;

    [SerializeField] private GameObject tree;

    public List<List<GameObject>> tiles = new List<List<GameObject>>();

    public int shoots;

    void Awake()
    {
        GenerateGrid();

        GenerateObstical(50, tree);
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
                    var x = Random.Range(0, width);
                    var y = Random.Range(0, height);

                    Vector2 size = item.transform.scale;

                    for(a = 0; a < size.x; a++)
                    {
                        for(b = 0; b < size.y; b++)
                        {
                            if(tiles[x + a][y + b].occupied)
                            {
                                tilesUnoccupied = false;
                            }
                        }
                    }

                    if(tilesUnoccupied)
                    {
                        GameObject newItem = Instantiate(item, new Vector3(x + (size.x - 1)/2, y + (size.y - 1)/2), transform.rotation);

                        for(a = 0; a < size.x; a++)
                        {
                            for(b = 0; b < size.y; b++)
                            tiles[x + a][y + b].GetComponent<TileScript>().occupied = true;
                        }

                        foundSpot = true;

                        newItem.transform.SetParent(gameObject.transform);
                    }
                }
            }
        }
}
