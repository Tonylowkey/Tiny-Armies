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
}
