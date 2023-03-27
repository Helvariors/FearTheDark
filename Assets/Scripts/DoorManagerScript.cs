using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManagerScript : MonoBehaviour
{
	public int doorInt = 0;
	public GameObject[] Doors;
	public int currentDoorInt;
	public DoorScript DoorScript;
	void ResetDoorInt()
	{
		doorInt = 0;
	}

	public void OpenNextDoor()
	{
		if(currentDoorInt + 1 < Doors.Length)
		{
			Debug.Log("Opening next door");
			doorInt = doorInt + 1;
			currentDoorInt = doorInt - 1;			
			Doors[currentDoorInt].GetComponent<DoorScript>().OpenDoor();
		}
	}
}
