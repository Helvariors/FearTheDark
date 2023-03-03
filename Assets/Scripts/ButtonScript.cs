using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
	public PlayerMovementScript PlayerMovementScript;
	public bool OptionsPMenuOn;
	public GameObject OptionsParent;
	public Toggle fSToggle;
	public Toggle vSToggle;
	public TMPro.TMP_Dropdown resDropdown;
	
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
			OptionsPMenuOn = true;
			OptionsParent.SetActive(true);
			
			if (PlayerPrefs.GetInt("fullscreenOn", 0) == 1)
			{
				fSToggle.isOn = true;
			}
			else
			{
				fSToggle.isOn = false;
			}
			
			if (PlayerPrefs.GetInt("vSyncOn", 0) == 1)
			{
				vSToggle.isOn = true;
			}
			else
			{
				vSToggle.isOn = false;
			}
			resDropdown.value = PlayerPrefs.GetInt("chosenScreenRes", 3);
		}
	}
	public void HideOptionsPMenu()
	{
			OptionsPMenuOn = false;
			OptionsParent.SetActive(false);
	}
}
