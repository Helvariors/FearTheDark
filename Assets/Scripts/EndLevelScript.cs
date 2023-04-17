using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelScript : MonoBehaviour
{
	BoxCollider2D EndLevelTrigger;
	void Start()
	{
		EndLevelTrigger = gameObject.GetComponent<BoxCollider2D>();
	}

	void OnTriggerEnter2D()
	{
		Debug.Log("invoke LoadEndScene");
		Invoke("LoadEndScreen", 2f);
	}
	void LoadEndScreen()
	{
		Debug.Log("LoadEndScene");
		SceneLoader.Load(SceneLoader.Scene.EndScreen);
	}
}
