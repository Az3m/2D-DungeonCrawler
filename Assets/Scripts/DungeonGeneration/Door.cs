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
}
