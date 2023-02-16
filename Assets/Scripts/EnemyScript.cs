using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	public List<Transform> PPs = new List<Transform>();
	public int PPIndex = 0;
	bool EcanMove = true;
	

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
			transform.position = Vector2.MoveTowards(transform.position, PPs[PPIndex].position,step);
			
			Vector3 dir = PPs[PPIndex].position - transform.position;
			Quaternion rot = Quaternion.LookRotation(Vector3.forward, dir);
			transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * turnSpeed);
		}

	}
	
	IEnumerator PPIndexer()
	{
		EcanMove = false;
		switch (gameObject.name)
		{
			case "Enemy (1)":
				yield return new WaitForSeconds(3);
				break;
		}
		EcanMove = true;
		yield return new WaitUntil(() => Vector3.Distance(transform.position , PPs[PPIndex].position) < 1.3);
		if (PPIndex < PPs.Count - 1)
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
		Debug.Log("Pspot");
	}
}
