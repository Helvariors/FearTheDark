using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManagerScript : MonoBehaviour
{
	public int doorInt = 0;
	GameObject[] Doors;
	public Hashtable doorsHash = new Hashtable();
	public int currentDoorInt;
	public DoorScript DoorScript;
	void Start()
	{
		ResetDoorInt();
		//find all doors in the level
		Doors = GameObject.FindGameObjectsWithTag("Door");
		int doorAdding = 0;
		//add all the doors into a hashtable
		foreach (GameObject Door in Doors)
		{
			//false = dvere zavrene
			doorsHash.Add(doorAdding, false);
			doorAdding++;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (doorsHash.ContainsValue(true))
		{
			Doors[currentDoorInt].GetComponent<DoorScript>().OpenDoor();
		}
	}
	void ResetDoorInt()
	{
		doorInt = 0;
	}

	public void OpenNextDoor()
	{
		if(currentDoorInt + 1 < Doors.Length)
		{
			doorsHash[doorInt] = true;
			doorInt = doorInt + 1;
			currentDoorInt = doorInt - 1;			
		}
	}
	public void CloseNextDoor()
	{
		doorsHash[doorInt] = false;
	}
}
