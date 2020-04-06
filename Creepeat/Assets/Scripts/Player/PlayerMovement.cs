using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	Camera usedCamera;

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
		yRot = transform.eulerAngles.y;
		rb = GetComponent<Rigidbody>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		GameManager.Instance.onGameOver += () =>
		{
			rb.isKinematic = true;
			enabled = false;
		};
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
		//if(Vector3.Angle(collision.impulse, Vector3.up) < 30)
			isGrounded = true;
	}

	private void OnCollisionExit(Collision collision)
	{
		isGrounded = false;
	}
}
