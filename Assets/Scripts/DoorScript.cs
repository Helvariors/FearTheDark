using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
	GameObject player;
	public ButtonScript ButtonScript;
	[SerializeField] string DoorObjTextEditor;
	public static string DoorObjTextMediator;
	GameObject DoorManager;

	void Update()
	{
		DoorObjTextMediator = DoorObjTextEditor;
		player = GameObject.Find("Player");
	}
	//If the player touches the door before it is opened it shows it's the condition to open it
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject == player)
		{
			ButtonScript.DoorObjTextShow();
		}
	}
	void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject == player)
		{
			ButtonScript.DoorObjTextHide();
		}
	}
	//open and close the door
	public void OpenDoor()
	{
		Debug.Log("Open Door");
		gameObject.SetActive(false);
	}
	public void CloseDoor()
	{
		gameObject.SetActive(true);
	}
}
