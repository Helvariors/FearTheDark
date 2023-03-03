using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSettingsScript : MonoBehaviour
{
	int screenWidth;
	int screenHeight;
	public bool fullscreenOn;
	public int chosenScreenRes;
	public Toggle fsToggle;
	public TMPro.TMP_Dropdown resDropdown;

	void Start()
	{
		
	}

	void Update()
	{
		
	}
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
	}
	public void SetScreenResolution()
	{
		fullscreenOn = fsToggle.isOn;
		//width height fullscreen
		Screen.SetResolution(screenWidth, screenHeight, fullscreenOn);
	}
}
