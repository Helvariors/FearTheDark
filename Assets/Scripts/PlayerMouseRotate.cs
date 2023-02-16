// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerMouseRotate : MonoBehaviour
// {
//     Vector3 mousePosition;
//     void Update()
//     {
//         mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//         Vector2 direction = (mousePosition - transform.position).normalized;
//         float angle = Vector2.SignedAngle(Vector2.up, direction);
//         transform.eulerAngles = new Vector3(0, 0, angle);
//     }
// }


// DEPRACTED!!! MOVED TO PlayerMovementScript