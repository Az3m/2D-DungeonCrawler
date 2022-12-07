using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;
    public Room currentRoom;
    public float cameraSpeedWhenRoomChange;

    private void Awake()
    {
        instance= this;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        if (currentRoom == null)
        {
            return;
        }

        Vector3 targetPos = GetCameraTargetPosition();

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime*cameraSpeedWhenRoomChange);
    }

    Vector3 GetCameraTargetPosition()
    {
        if (currentRoom == null)
        {
            return Vector3.zero;
        }

        Vector3 targetPos = currentRoom.GetRoomCenter();
        targetPos.z = transform.position.z;

        return targetPos;
    }

    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
}
