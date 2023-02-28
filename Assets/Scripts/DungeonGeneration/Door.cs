using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//script folosit pentru a clasifica usile fiecarei camere
//o usa poate fi : sus,jos,stanga sau dreapta
public class Door : MonoBehaviour
{
   public enum DoorType
    {
        left,right, top, bottom
    }

    public DoorType doorType;
    private GameObject player;
    private float widthOffset = 0;
    public GameObject DoorCollider;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            switch(doorType)
            {
                case DoorType.bottom:
                    player.transform.position = new Vector2(transform.position.x, transform.position.y - widthOffset);
                break;

                case DoorType.top:
                    player.transform.position = new Vector2(transform.position.x, transform.position.y + widthOffset);
                break;

                case DoorType.left:
                    player.transform.position = new Vector2(transform.position.x - widthOffset, transform.position.y);
                break;

                case DoorType.right:
                    player.transform.position = new Vector2(transform.position.x + widthOffset, transform.position.y);
                break;


            }
        }
    }
}
