using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
	public enum Scene
	{
		MainMenu, 
		EndScreen, 
		SampleScene
	}
	public static void Load(Scene scene)
	{
		Debug.Log("LoadScene");
		SceneManager.LoadScene(scene.ToString());
		Debug.Log("Loaded scene: " + scene.ToString());
	}
}
