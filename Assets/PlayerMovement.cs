using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController controller;
	public Transform groundCheck;

	public float speed = 12f;
	public float g = -9.81f;
	public float gDist = 0.4f;
	public float jumpH = 3f;

	public LayerMask gMask;

	Vector3 v;
	bool isGrounded;

    // Update is called once per frame
    void Update()
    {
    	isGrounded = Physics.CheckSphere(groundCheck.position, gDist, gMask);

    	if (isGrounded && v.y < 0 ) {
    		v.y = -2f;
    	}

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) {
        	v.y = Mathf.Sqrt(jumpH * -2f * g);
        }

        v.y += g * Time.deltaTime;

        controller.Move(v * Time.deltaTime);
    }
}
