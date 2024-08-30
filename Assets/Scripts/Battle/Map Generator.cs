using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width = 5;
    public int length = 5;
    float distBetweenTile = 1f;
    List<List<int>> mapArray;

    //All the types of tile types
    public GameObject lowTile;
    public GameObject medTile;
    public GameObject highTile;

    void Start()
    {
        // Initialize the nested list
        InitializeNestedList();
        //Debug.Log(GetElement(0,0));
        setTiles(mapArray);
        
    }

    void InitializeNestedList()
    {
        // Create the outer list with specified length (y)
        mapArray = new List<List<int>>(length);

        // Populate the outer list with inner lists (rows)
        for (int i = 0; i < length; i++)
        {
            mapArray.Add(new List<int>(width));
        }

        // Initialize inner lists with default values (e.g., 0)
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int x = Random.Range(0, 15);
                if(x < 12)
                {
                    mapArray[i].Add(1);
                }
                else if(x < 13)
                {
                    mapArray[i].Add(2);
                }
                else if(x < 15)
                {
                    mapArray[i].Add(3);
                }
                else
                {
                    mapArray[i].Add(4);
                }
                
            }
        }
    }

    int GetElement(int x, int y)
    {
        // Access element in the nested list
        return mapArray[y][x];
    }

    void SetElement(int x, int y, int value)
    {
        // Modify element in the nested list
        mapArray[y][x] = value;
    }

    private void setTiles(List<List<int>> mapArray)
    {
        for(int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int tileType = GetElement(j, i);
                //Debug.Log("j: " + j + " i: " + i + " " + tileType);
                if (tileType == 1)
                {
                    Instantiate(lowTile, new Vector3(distBetweenTile * (float)i, 0f, distBetweenTile * (float)j), Quaternion.identity);
                }
                else if (tileType == 2)
                {
                    Instantiate(medTile, new Vector3(distBetweenTile * (float)i, 0f, distBetweenTile * (float)j), Quaternion.identity);
                }
                else if (tileType == 3)
                {
                    Instantiate(highTile, new Vector3(distBetweenTile * (float)i, 0f, distBetweenTile * (float)j), Quaternion.identity);
                }
                else if (tileType == 4)
                {
                    //Instantiate(medTileTile, new Vector3(distBetweenTile * (float)i, 0f, distBetweenTile * (float)j), Quaternion.identity);
                }
            }
        }
    }
}
