using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class GridController : MonoBehaviour
{
    public Room room;

    [System.Serializable]
    public struct Grid
    {
        public int columns, rows;
        public float verticalOffset, horizontalOffset;
    }

    public Grid grid;
    public GameObject gridTile;
    public List<Vector2> availablePoints = new List<Vector2>();


    private void Awake()
    {
        room = GetComponentInParent<Room>();
        grid.columns = room.Width - 2;
        grid.rows = room.Height - 1;
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        grid.verticalOffset += room.transform.localPosition.y;
        grid.horizontalOffset += room.transform.localPosition.x;
        var pozitiiInvalide = new List <(int, int)> // acestea sunt toate pozitiile in care nu se pot spawnla monstrii sau iteme
        {   //usa jos
            (6,0),(7,0),(8,0),(9,0),(10,0),(6,1),(7,1),(8,1),(9,1),(10,1),

            //usa stanga
            (0,3),(1,3),(2,3),(0,4),(1,4),(2,4),(0,5),(1,5),(2,5),

            //usa sus
            (6,7),(7,7),(8,7),(9,7),(10,7),(6,8),(7,8),(8,8),(9,8),(10,8),

            //usa dreapta
            (13,3),(14,3),(15,3),(13,4),(14,4),(15,4),(13,5),(14,5),(15,5)
        };

        for (int y = 0; y < grid.rows; y++)
        {
            for(int x = 0; x < grid.columns; x++)
            {
                if (pozitiiInvalide.Any(t => t.Item1 == x && t.Item2 == y))
                {
                    
                }
                else
                {
                    GameObject go = Instantiate(gridTile, transform);
                    go.GetComponent<Transform>().position = new Vector2(x - (grid.columns - grid.horizontalOffset), y - (grid.rows - grid.verticalOffset));
                    go.name = "X: " + x + "Y: " + y;
                    availablePoints.Add(go.transform.position);
                    //go.SetActive(false);
                }
            }
                
        }

            GetComponentInParent<ObjectRoomSpawner>().InitializeObjectSpawning();
        
    }
}
