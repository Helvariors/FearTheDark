using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class CameraScript : MonoBehaviour
{
	public GameObject player;
	public float smoothSpeed = 8f;

	void FixedUpdate()
	{
		Vector3 desiredPosition = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
		Vector3 smootherPosition = Vector3.Slerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
		transform.position = smootherPosition;
	}
}
