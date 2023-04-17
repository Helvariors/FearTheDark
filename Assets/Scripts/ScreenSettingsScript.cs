using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSettingsScript : MonoBehaviour
{
	int screenWidth;
	int screenHeight;
	public bool fullscreenOn;
	public bool vSyncOn;
	public int chosenScreenRes;
	public Toggle fsToggle;
	public TMPro.TMP_Dropdown resDropdown;

	public void ChooseScreenResolution()
	{
		chosenScreenRes = resDropdown.value;
		switch (chosenScreenRes)
		{
			case 0:
				screenWidth = 1280;
				screenHeight = 720;
				break;
			case 1:
				screenWidth = 1280;
				screenHeight = 1080;
				break;
			case 2:
				screenWidth = 1440;
				screenHeight = 1080;
				break;
			case 3:
				screenWidth = 1920;
				screenHeight = 1080;
				break;
			case 4:
				screenWidth = 2560;
				screenHeight = 1440;
				break;
			default:
				screenWidth = 1920;
				screenHeight = 1080;
				break;
		}
		SetVidSettings();
	}
	public void SetVidSettings()
	{
		fullscreenOn = fsToggle.isOn;
		//width height fullscreen
		Screen.SetResolution(screenWidth, screenHeight, fullscreenOn);
		Debug.Log(screenWidth);
		SaveVidSettings();
	}
	public void SaveVidSettings()
	{
		if (fullscreenOn)
		{
			PlayerPrefs.SetInt("fullscreenOn", 1);
		}
		else
		{
			PlayerPrefs.SetInt("fullscreenOn", 0);
		}
		
		if (vSyncOn)
		{
			PlayerPrefs.SetInt("vSyncOn", 1);
		}
		else
		{
			PlayerPrefs.SetInt("vSyncOn", 0);
		}
		
		PlayerPrefs.SetInt("chosenScreenRes", chosenScreenRes);
	}
}
