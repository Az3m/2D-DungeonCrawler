using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadRoom("Start", 0, 0);
        LoadRoom("Empty", 1, 0);
        LoadRoom("Empty", -1, 0);
        LoadRoom("Empty", 0, 1);
        LoadRoom("Empty", 0, -1);
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
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;
        
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
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
        room.transform.position = new Vector3(
            currentLoadRoomData.x * room.Width,
            currentLoadRoomData.y * room.Height,
            0);

        room.X = currentLoadRoomData.x;
        room.Y = currentLoadRoomData.y;
        room.name = currentWorldName + " - " + currentLoadRoomData.name + ' ' + room.X + "," + room.Y;
        room.transform.parent = transform;

        isLoadingRoom = false;

        loadedRooms.Add(room);
    }

    public bool DoesRoomExist (int x,int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y ) != null;
    }

}
