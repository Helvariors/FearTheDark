using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeScript : MonoBehaviour
{
	public LayerMask Pmask;
	float enemyRange = 10;	
	public EnemyScript enemyScript;
	void Start()
	{

	}

	void Update()
	{

	}
	void OnTriggerStay2D(Collider2D col)   
	{
		if (col.gameObject.tag == "Player")
		{
			if (gameObject.name == "coneDeath")
			{
				GameObject playerPos = GameObject.FindGameObjectWithTag("Player");
				RaycastHit2D playerCheck = Physics2D.Raycast(enemyScript.gameObject.transform.position, playerPos.transform.position - enemyScript.gameObject.transform.position, Mathf.Clamp(enemyRange, 0, Vector3.Distance(playerPos.transform.position, transform.position)), Pmask);
				if(playerCheck.collider == null)
				{
					Debug.DrawRay(enemyScript.gameObject.transform.position, (playerPos.transform.position - enemyScript.gameObject.transform.position).normalized * enemyRange, Color.red);
					enemyScript.PDeath(col.gameObject);
				}
			}
			
			if (gameObject.name == "coneSpot")
			{
				GameObject playerPos = GameObject.FindGameObjectWithTag("Player");
				RaycastHit2D playerCheck = Physics2D.Raycast(enemyScript.gameObject.transform.position, playerPos.transform.position - enemyScript.gameObject.transform.position, Mathf.Clamp(enemyRange, 0, Vector3.Distance(playerPos.transform.position, transform.position)), Pmask);
				if(playerCheck.collider == null)
				{
					Debug.DrawRay(enemyScript.gameObject.transform.position, (playerPos.transform.position - enemyScript.gameObject.transform.position).normalized * enemyRange, Color.yellow);
					enemyScript.PSpotted();
				}
			}
		}
	}
}
