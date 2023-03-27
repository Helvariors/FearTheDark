using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerScript : MonoBehaviour
{
	[SerializeField] GameObject[] EnemyBatches;
	Hashtable EnemyBatchesHash = new Hashtable();
	int EnemyBatchCheck = 0;
	//Hashtable EnemiesInCurrentBatch = new Hashtable();
	int enemiesInCurrentBatchCheck;
	[SerializeField] int currentEnemyBatch = 0;
	public DoorManagerScript DoorManagerScript;
	[SerializeField] int maxEnemyBatch;
	bool lastDoorOpened;
	void Start()
	{
		lastDoorOpened = false;
		foreach (GameObject EnemyBatch in EnemyBatches)
		{
			EnemyBatchesHash.Add(EnemyBatchCheck, true);
			EnemyBatchCheck++;
		}
	}
	void Update()
	{
		if(EnemyBatches[currentEnemyBatch].transform.childCount == 0 && !lastDoorOpened)
		{
			Debug.Log("open next door");
			DoorManagerScript.OpenNextDoor();
			MoveToNextBatch();
		}
	}
	void MoveToNextBatch()
	{
		if (currentEnemyBatch + 1 < EnemyBatches.Length)
		{
			Debug.Log("Moving to next enemy batch");
			currentEnemyBatch++;
		}
		else
		{
			lastDoorOpened = true;
			Debug.Log("No more doors to open");
		}
	}
}
