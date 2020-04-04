﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	Camera usedCamera;
    [SerializeField]
    Texture2D splashTexture;

    [SerializeField]
	float maxSpeed;
	[SerializeField]
	float acceleration;
	[SerializeField]
	float stopSpeed;
	[SerializeField]
	float rotationSpeed;
	[SerializeField]
	float maxXRot;

	float yRot = 0;
	float xRot = 0;
	Quaternion yQuat = Quaternion.identity;
	Quaternion xQuat = Quaternion.identity;

	Rigidbody rb;

	bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if(isGrounded)
			UpdateMove();
    }

	private void LateUpdate()
	{
		UpdateRotation();
        SplashFlour();

    }

    private void UpdateMove()
	{
		Vector3 moveInput = new Vector3(
		Input.GetAxisRaw("Horizontal")
		, 0,
		Input.GetAxisRaw("Vertical")
		);

		if (moveInput.sqrMagnitude != 0)
		{
			moveInput.Normalize();
			rb.AddForce(yQuat * moveInput * acceleration * Time.deltaTime, ForceMode.VelocityChange);
			if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed)
				rb.velocity = rb.velocity.normalized * maxSpeed;
		}
		else
		{
			if (stopSpeed * Time.deltaTime < 1)
				rb.velocity -= rb.velocity * stopSpeed * Time.deltaTime;
			else
				rb.velocity = Vector3.zero;
		}
	}

    private void SplashFlour()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = usedCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                SplashObject script = hit.collider.gameObject.GetComponent<SplashObject>();
                if (null != script)
                    script.PaintOn(hit.textureCoord, splashTexture);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Ray ray = usedCamera.ScreenPointToRay(Input.mousePosition);
        Gizmos.color = Color.black;
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 20);
    }

	private void UpdateRotation()
	{
		float mouseX = Input.GetAxisRaw("Mouse X");
		float mouseY = Input.GetAxisRaw("Mouse Y");

		yRot += (mouseX) * rotationSpeed;
		yRot = yRot % 360;

		xRot += (mouseY) * rotationSpeed;
		if (xRot < -maxXRot)
			xRot = -maxXRot;
		if (xRot > maxXRot)
			xRot = maxXRot;

		yQuat = Quaternion.AngleAxis(yRot, Vector3.up);
		xQuat = Quaternion.AngleAxis(xRot, Vector3.left);

		usedCamera.transform.localRotation = xQuat;
		transform.localRotation = yQuat;
	}

	private void OnCollisionStay(Collision collision)
	{
		isGrounded = true;
	}

	private void OnCollisionExit(Collision collision)
	{
		isGrounded = false;
	}
}
