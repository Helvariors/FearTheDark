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
	void Start()
	{
		foreach (GameObject EnemyBatch in EnemyBatches)
		{
			EnemyBatchesHash.Add(EnemyBatchCheck, true);
			EnemyBatchCheck++;
		}
	}
	void Update()
	{
		if(EnemyBatches[currentEnemyBatch].transform.childCount == 0)
		{
			DoorManagerScript.OpenNextDoor();
			MoveToNextBatch();
		}
		
	}
	void MoveToNextBatch()
	{
		if (currentEnemyBatch + 1 < EnemyBatches.Length)
		{
			currentEnemyBatch++;
		}
	}
}
