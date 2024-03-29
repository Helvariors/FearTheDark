using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour
{
	Rigidbody2D prb;
	bool lockmovement = false;
	float playerPosition;
	public float movementSpeed = 5f;
	Vector2 movement;
	float moveMod;
	float AttackTime = 0f;
	float AttackCD = 0.75f;
	public Wpn_anim wpn_anim;
	public LayerMask lightMask;
	GameObject[] lights;
	public static bool isInStealth;
	public static bool mouseStealth;
	public Hashtable lightsHash = new Hashtable();
	public GameObject debugText;
	Vector3 mousePosition;
	Vector3 mousePositionTrue;
	public Hashtable mouseLightsHash = new Hashtable();
	public Animator PauseMenuAnim;
	public bool PauseMenuOn;
	
	GameObject mouseLightCheckPositions;
	GameObject mouseLightCheckPosTop;
	GameObject mouseLightCheckPosLeft;
	GameObject mouseLightCheckPosRight;
	GameObject mouseLightCheckPosBottom;
	bool DebugEnabled;
	[SerializeField] GameObject DebugMenu;
	[SerializeField] Animator PlayerAnimator;
	bool TpDown;
	[SerializeField] GameObject playerPoint;
	public CameraScript CameraScript;
	[SerializeField] GameObject StealthEyeOpened;
	[SerializeField] GameObject StealthEyeClosed;

	void Start()
	{
		DisableDebug();
		//set the rigidbody component
		prb = gameObject.GetComponent<Rigidbody2D>();
		lights = GameObject.FindGameObjectsWithTag("Light");
		int i = 0;
		foreach (GameObject Light in lights)
		{
			lightsHash.Add(i, 1);
			mouseLightsHash.Add(i, 1);
			i++;
		}
		mouseLightCheckPositions = GameObject.Find("MouseLightCheckPositions");
		mouseLightCheckPosTop = GameObject.Find("MouseLightCheckPositions/MouseLightCheckPosTop");
		mouseLightCheckPosLeft = GameObject.Find("MouseLightCheckPositions/MouseLightCheckPosLeft");
		mouseLightCheckPosRight = GameObject.Find("MouseLightCheckPositions/MouseLightCheckPosRight");
		mouseLightCheckPosBottom = GameObject.Find("MouseLightCheckPositions/MouseLightCheckPosBottom");
	}

	void Update()
	{
		Debug.DrawRay(playerPoint.transform.position, mousePositionTrue - playerPoint.transform.position, Color.blue);
		if(Input.GetKeyDown(KeyCode.P))
		{
			if (DebugEnabled)
			{
				DebugEnabled = false;
				DisableDebug();
			}
			else
			{
				DebugEnabled = true;
				EnableDebug();
			}
		}
		debugText.GetComponent<Text>().text = " Light0 " + lightsHash[0] + "\n Light1 " + lightsHash[1] + "\n Light2 " + lightsHash[2] + "\n Light3" + lightsHash[3] + "\n Light4" + lightsHash[4] + "\n ML" + mouseStealth + "\n ML0 " + mouseLightsHash[0] + "\n ML1 " + mouseLightsHash[1] + "\n ML2 " + mouseLightsHash[2] + "\n ML3 " + mouseLightsHash[3] + "\n ML4 " + mouseLightsHash[4];
		//get movement input
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");
		//check for diagonal movement
		if (movement.x != 0 && movement.y != 0)
		{
			moveMod = 0.70710678118f;
			//protoze pythagoras
		}
		//if no diagonal movement is found
		else
		{
			moveMod = 1f;
		}

		//check for locking movement keypress
		if (Input.GetKeyDown(KeyCode.F))
		{
			LockMovement();
		}
		//rotate player to mouse, also check for locked movement -----------------
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePositionTrue = new Vector3(mousePosition.x, mousePosition.y, -1);
		if(lockmovement == false)
		{
			if (Input.GetMouseButtonDown(0) && Time.time - AttackTime > AttackCD)
			{
				AttackTime = Time.time;
				wpn_anim.PAttack();
			}
	   	 	Vector2 direction = (mousePosition - transform.position).normalized;
			float angle = Vector2.SignedAngle(Vector2.up, direction);
			transform.eulerAngles = new Vector3(0, 0, angle);
		}
		//raycasting to lights to check when player is in shadows--------------------------------
		int y = 0;
		foreach (GameObject Light in lights)
		{
			if (Vector3.Distance(transform.position, Light.transform.position) < Light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius)
			{	

				Transform lightPosCheckTop = GameObject.Find("LightCheckPosTop").transform;//transform.Find("LightCheckPosTop");
				RaycastHit2D stealthHit1 = Physics2D.Raycast(Light.transform.position, lightPosCheckTop.position - Light.transform.position, Mathf.Clamp(Light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius, 0, Vector3.Distance(lightPosCheckTop.position, Light.transform.position)));
				
				Transform lightPosCheckLeft = GameObject.Find("LightCheckPosLeft").transform;
				RaycastHit2D stealthHit2 = Physics2D.Raycast(Light.transform.position, lightPosCheckLeft.position - Light.transform.position, Mathf.Clamp(Light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius, 0, Vector3.Distance(lightPosCheckLeft.position, Light.transform.position)));
				
				Transform lightPosCheckRight = GameObject.Find("LightCheckPosRight").transform;
				RaycastHit2D stealthHit3 = Physics2D.Raycast(Light.transform.position, lightPosCheckRight.position - Light.transform.position, Mathf.Clamp(Light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius, 0, Vector3.Distance(lightPosCheckRight.position, Light.transform.position)));
				
				Transform lightPosCheckBottom = GameObject.Find("LightCheckPosBottom").transform;
				RaycastHit2D stealthHit4 = Physics2D.Raycast(Light.transform.position, lightPosCheckBottom.position - Light.transform.position, Mathf.Clamp(Light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius, 0, Vector3.Distance(lightPosCheckBottom.position, Light.transform.position)));

				if (stealthHit1.collider == null || stealthHit2.collider == null || stealthHit3.collider == null || stealthHit4.collider == null)
				{
					lightsHash[y] = 0;
				}
				else
				{
					Debug.DrawRay(Light.transform.position, (transform.position - Light.transform.position).normalized * Light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius, Color.green);
					lightsHash[y] = 1;
				}
				//RaycastHit2D stealthHit = Physics2D.Raycast(Light.transform.position, transform.position - Light.transform.position, Mathf.Clamp(Light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius, 0, Vector3.Distance(transform.position, Light.transform.position)));
				//sem pak dej switch kde skopriujes jenom to nad timhle komentem, top left right bottom podle i ve for loopu
			
			}
			else
			{
				lightsHash[y] = 1;
			}
			//mouse stealth check ///////////////////////
			if (Vector3.Distance(mousePositionTrue, Light.transform.position) < Light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius)
			{
				RaycastHit2D mouseStealthHit1 = Physics2D.Raycast(Light.transform.position, mouseLightCheckPosTop.transform.position - Light.transform.position,  Mathf.Clamp(Light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius, 0, Vector3.Distance(mouseLightCheckPosTop.transform.position, Light.transform.position)));
				RaycastHit2D mouseStealthHit2 = Physics2D.Raycast(Light.transform.position, mouseLightCheckPosLeft.transform.position - Light.transform.position,  Mathf.Clamp(Light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius, 0, Vector3.Distance(mouseLightCheckPosLeft.transform.position, Light.transform.position)));
				RaycastHit2D mouseStealthHit3 = Physics2D.Raycast(Light.transform.position, mouseLightCheckPosRight.transform.position - Light.transform.position,  Mathf.Clamp(Light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius, 0, Vector3.Distance(mouseLightCheckPosRight.transform.position, Light.transform.position)));
				RaycastHit2D mouseStealthHit4 = Physics2D.Raycast(Light.transform.position, mouseLightCheckPosBottom.transform.position - Light.transform.position,  Mathf.Clamp(Light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius, 0, Vector3.Distance(mouseLightCheckPosBottom.transform.position, Light.transform.position)));
				
				if (mouseStealthHit1.collider == null || mouseStealthHit2.collider == null || mouseStealthHit3.collider == null || mouseStealthHit4.collider == null)
				{
					mouseLightsHash[y] = 0;
				}
				else
				{
					mouseLightsHash[y] = 1;
				}
			}
			else
			{
				mouseLightsHash[y] = 1;
			}
			y++;
			//mouse stealth check end ///////////////////
		}
		if (lightsHash.ContainsValue(0))
		{
			isInStealth = false;
		}
		else // VE STEALTHU JE 1!!
		{
			isInStealth = true;
		}
		if (mouseLightsHash.ContainsValue(0))
		{
			mouseStealth = false;
		}
		else // VE STEALTHU JE 1!!
		{
			mouseStealth = true;
		}
		//ShadowStepping
		if (isInStealth == true && mouseStealth == true )
		{
			TpDown = PlayerAnimator.GetBool("TpReady");
			if (Input.GetMouseButtonDown(1) && TpDown == false)
			{
				ShadowStep();
			}
		}
		//stealth check end -------------------------------------
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (PauseMenuOn)
			{
				HidePauseMenu();
			}
			else
			{
				ShowPauseMenu();
			}
		}
		//mouse stealth checkers moving to mouse pos//
		mouseLightCheckPositions.transform.position = mousePositionTrue;
		if (isInStealth)
		{
			StealthEyeOpened.SetActive(false);
			StealthEyeClosed.SetActive(true);
		}
		else
		{
			StealthEyeOpened.SetActive(true);
			StealthEyeClosed.SetActive(false);
		}
	}
	void FixedUpdate() 
	{
		if (lockmovement == false)
		{
			prb.MovePosition(prb.position + movement * movementSpeed * moveMod * Time.fixedDeltaTime);
		}
	}
	//void for locking the movement (and rotation) of the player----------------
	public void LockMovement()
	{
		if (!lockmovement)
		{ 
			lockmovement = true;
			Debug.Log("Locked movement");
		}
		else
		{
			lockmovement = false;
			Debug.Log("Unlocked movement");
		}
	}
	public void HidePauseMenu()
	{
		PauseMenuOn = false;
		PauseMenuAnim.SetBool("ShowMenu", false);
		lockmovement = false;
		Time.timeScale = 1f;
	}
	public void ShowPauseMenu()
	{
		PauseMenuOn = true;
		PauseMenuAnim.SetBool("ShowMenu", true);
		lockmovement = true;
		Time.timeScale = 0f;
	}
	public void EnableDebug()
	{
		DebugMenu.SetActive(true);
	}
	public void DisableDebug()
	{
		DebugMenu.SetActive(false);
	}
	void ShadowStep()
	{
		RaycastHit2D CanShadowStep = Physics2D.Raycast(playerPoint.transform.position, mousePositionTrue - playerPoint.transform.position, Vector2.Distance(mousePositionTrue, playerPoint.transform.position));
		if (CanShadowStep.collider == null)
		{
			transform.position = new Vector3(mousePosition.x,mousePosition.y,-1);
			PlayerAnimator.SetBool("TpReady", true);
			Debug.Log("ShadowStep Activated");
			Invoke("ShadowStepEnd", 0.35f);
		}
		else
		{
			Debug.Log("Can't ShadowStep");
		}
	}
	void ShadowStepEnd()
	{
		PlayerAnimator.SetBool("TpReady", false);
	}
}
