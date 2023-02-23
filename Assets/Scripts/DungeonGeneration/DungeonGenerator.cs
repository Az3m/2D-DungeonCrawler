using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms;



    private void Start()
    {
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);

    }

   


    private void SpawnRooms (IEnumerable<Vector2Int> rooms)
    {
        //Creaza o camera de inceput "Start" si pentru fiecare crawler creaza cate o camera in fiecare directie pe care acestia o aleg
        RoomController.instance.LoadRoom("Start", 0, 0);

        foreach(Vector2Int roomLocation in rooms)
        {
            if (roomLocation == dungeonRooms[dungeonRooms.Count - 1] && !(roomLocation == Vector2Int.zero))
            {
                RoomController.instance.LoadRoom("End",roomLocation.x, roomLocation.y);

            }
            else {
                RoomController.instance.LoadRoom("Empty", roomLocation.x, roomLocation.y);
            }
        }
    }
}
