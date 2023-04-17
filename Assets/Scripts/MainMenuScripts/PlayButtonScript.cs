using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonScript : MonoBehaviour
{
	public void StartGame()
	{
		Debug.Log("Starting the first level");
		SceneLoader.Load(SceneLoader.Scene.SampleScene);
	}
}
