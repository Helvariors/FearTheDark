using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
	public PlayerMovementScript PlayerMovementScript;
	public bool OptionsPMenuOn;
	public GameObject OptionsParent;
	void Start()
	{
		GameObject OptionsButton = GameObject.Find("PauseMenu/OptionsButton");
		GameObject ETMButton = GameObject.Find("PauseMenu/ETMButton");
		GameObject QuitButton = GameObject.Find("PauseMenu/QuitButton");
		OptionsParent = GameObject.Find("PauseMenu/Options/OptionsParent");
	}

	void Update()
	{
		if (PlayerMovementScript.PauseMenuOn == false)
		{
			HideOptionsPMenu();
		}
	}
	public void DecideOptionsPMenu()
	{
		if (OptionsPMenuOn == false)
		{
			ShowOptionsPMenu();
		}
		else
		{
			HideOptionsPMenu();
		}
	}
	public void ShowOptionsPMenu()
	{
		if (PlayerMovementScript.PauseMenuOn == true)
		{
			Debug.Log("Opened options menu");
			OptionsPMenuOn = true;
			OptionsParent.SetActive(true);
		}
	}
	public void HideOptionsPMenu()
	{
			Debug.Log("Closed options menu");
			OptionsPMenuOn = false;
			OptionsParent.SetActive(false);
	}
}
