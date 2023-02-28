using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Linq;
using UnityEngine.Rendering;

public class RoomInfo
{
    public string name;
    public int x;
    public int y;
}

public class RoomController : MonoBehaviour
{

    public static RoomController instance;

    string currentWorldName = "Basement";

    RoomInfo currentLoadRoomData;

    Room currentRoom;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;
    bool spawnedBossRoom = false;
    bool updatedRooms = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
        //LoadRoom("Start", 0, 0);
        //LoadRoom("Empty", 1, 0);
        //LoadRoom("Empty", -1, 0);
        //LoadRoom("Empty", 0, 1);
        //LoadRoom("Empty", 0, -1);
        
    }

    private void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if(isLoadingRoom)
        {
            return;
        }
        if (loadRoomQueue.Count == 0)
        {
            if (!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            } 
            else if (spawnedBossRoom && !updatedRooms)
            {
                foreach (Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }

                UpdateRooms();
                updatedRooms = true;
            }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;
        
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        int loadedRoomsCount = loadedRooms.Count;
        yield return new WaitForSeconds(0.5f);// astept jumatate de secunda sa fiu sigur ca toate camerele au fost create


        if (loadRoomQueue.Count == 0)
        {
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Room tempRoom = new Room(bossRoom.X, bossRoom.Y);

            Destroy(bossRoom.gameObject);

            var roomToRemove = loadedRooms.Single(room => room.X == tempRoom.X && room.Y == tempRoom.Y); //AICI MODIFIC CUM SE GENEREAZA CAMERA BOSSULUI

            loadedRooms.Remove(roomToRemove);

            LoadRoom("End", tempRoom.X, tempRoom.Y);
        }
    }

    public void LoadRoom(string name,int x, int y)
    {
        if (DoesRoomExist(x, y)){
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.x = x;
        newRoomData.y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName,LoadSceneMode.Additive);

        while(loadRoom.isDone ==false)
        {
            yield return null;
        }
    }

    public void RegisterRoom (Room room)
    {
        if (!DoesRoomExist(currentLoadRoomData.x, currentLoadRoomData.y)){
            room.transform.position = new Vector3(
                currentLoadRoomData.x * room.Width,
                currentLoadRoomData.y * room.Height,
                0);

            room.X = currentLoadRoomData.x;
            room.Y = currentLoadRoomData.y;
            room.name = currentWorldName + " - " + currentLoadRoomData.name + ' ' + room.X + "," + room.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;

            if (loadedRooms.Count == 0)
            {
                CameraController.instance.currentRoom = room;
            }

            loadedRooms.Add(room);
            
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }

    public bool DoesRoomExist (int x,int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y ) != null;
    }

    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }

    public string GetRandomRegularRoomName()
    {
        string[] possibleRooms = new string[]
        {
            "Empty",
            "Basic1"
        };

        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currentRoom = room;
        currentRoom = room;

        StartCoroutine(RoomCoroutine());
    }

    public IEnumerator RoomCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        UpdateRooms();
    }
    public void UpdateRooms()
    {
        foreach (Room room in loadedRooms)
        {
            if (currentRoom != room)
            {
                EnemyController[] enemies = room.GetComponentsInChildren<EnemyController>();
                if (enemies != null)
                {
                    foreach (EnemyController enemy in enemies)
                    {
                        enemy.notInRoom = true;
                        Debug.Log("Not in room");
                    }

                    foreach(Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.DoorCollider.SetActive(false);
                    }
                }
                else{
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.DoorCollider.SetActive(true);
                    }
                }
            }
            else
            {
                EnemyController[] enemies = room.GetComponentsInChildren<EnemyController>();
                if (enemies.Length > 0)
                {
                    foreach (EnemyController enemy in enemies)
                    {
                        enemy.notInRoom = false;
                        Debug.Log("In room");
                    }

                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.DoorCollider.SetActive(true);
                    }
                }
                else
                {
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.DoorCollider.SetActive(false);
                    }
                }
            }
        }
    }
}
