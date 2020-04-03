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

	Quaternion yQuat = Quaternion.identity;
	Quaternion xQuat = Quaternion.identity;

	Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		UpdateMove();
    }

	private void Update()
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

		yQuat = yQuat * Quaternion.AngleAxis(mouseX * rotationSpeed * Time.deltaTime, Vector3.up);
		xQuat = xQuat * Quaternion.AngleAxis(mouseY * rotationSpeed * Time.deltaTime, Vector3.left);

		usedCamera.transform.localRotation = xQuat;
		transform.localRotation = yQuat;
	}
}
