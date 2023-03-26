using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	public List<Transform> PPs = new List<Transform>();
	public int PPIndex = 0;
	bool EcanMove = true;
	public List<Transform> wayPoints = new List<Transform>();
	public bool playerSeen = false;
	List<Transform> Temp = new List<Transform>();
	GameObject player;
	public EnemyManagerScript EnemyManagerScript;
	

	void Start()
	{
		StartCoroutine(PPIndexer());
	}
	void Update()
	{
		//Debug.Log(Vector3.Distance(transform.position , PPs[PPIndex].position));
		float speed = 1;
		float turnSpeed = 2;
		float step = speed * Time.deltaTime;
		
		if(EcanMove)
		{
			transform.position = Vector2.MoveTowards(transform.position, Temp[PPIndex].position,step);
			
			Vector3 dir = Temp[PPIndex].position - transform.position;
			Quaternion rot = Quaternion.LookRotation(Vector3.forward, dir);
			transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
		}


	}
	IEnumerator PlayerFollow()
	{
		player = GameObject.Find("Player");
		if (playerSeen && !PlayerMovementScript.isInStealth)
		{
			wayPoints.Add(player.transform);
			yield return new WaitForSeconds(0.5f);
			StartCoroutine(PlayerFollow());
		}
		else if(playerSeen && PlayerMovementScript.isInStealth)
		{
			playerSeen = false;
			PPIndex = 0;
			wayPoints = new List<Transform>();
		}
	}
	IEnumerator AlertWait()
	{
		yield return new WaitForSeconds(10);
		Debug.Log("Player lost");
		transform.GetChild(1).GetComponent<PolygonCollider2D>().enabled = false;
		transform.GetChild(1).GetComponent<PolygonCollider2D>().enabled = true;
		playerSeen = false;
		PPIndex = 0;
		wayPoints = new List<Transform>();
	}
	IEnumerator PPIndexer()
	{
		if (playerSeen)
		{
			Temp = wayPoints;
		}
		else
		{
			Temp = PPs;
		}
		EcanMove = false;
		if(!playerSeen)
		{
			switch (gameObject.name)
			{
				case "Enemy (1)":
					yield return new WaitForSeconds(3);
					break;
			}
		} 
		EcanMove = true;
		if (playerSeen)
		{
			yield return new WaitUntil(() => Vector3.Distance(transform.position , Temp[PPIndex].position) < 1.3 || !playerSeen);
		}
		else
		{
			yield return new WaitUntil(() => Vector3.Distance(transform.position , Temp[PPIndex].position) < 1.3 || playerSeen);
		}
		if (PPIndex < Temp.Count - 1)
		{
			PPIndex++;
		}
		else
		{
			PPIndex = 0;
		}
		StartCoroutine(PPIndexer());
	}



	void OnCollisionEnter2D(Collision2D col)
	{
		PDeath(col.gameObject);
	}
	
	public void PDeath(GameObject col)
	{
		if (col.tag == "Player" && PlayerMovementScript.isInStealth == false)
		{
			Debug.Log("Player killed");
			SceneLoader.Load(SceneLoader.Scene.SampleScene);
		} 
	}

	public void Death()
	{
		Destroy(gameObject);
	}
	public void PSpotted()
	{
		player = GameObject.Find("Player");
		wayPoints.Add(player.transform);
		PPIndex = 0;
		Debug.Log("Player Spotted");
		playerSeen = true;
		StartCoroutine(PlayerFollow());
		StartCoroutine(AlertWait());
	}
}
