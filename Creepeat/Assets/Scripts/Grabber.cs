using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
	Joint joint = null;

	Grabbable grabbable = null;
	bool isGrabbing = false;

	[SerializeField]
	bool useJoint = true;
	[SerializeField]
	Vector3 grabOffset;

	void Update()
    {
        if(Input.GetButtonDown("Use"))
		{
			if(grabbable)
			{
				if (!isGrabbing)
				{
					if(useJoint)
					{
						joint = gameObject.AddComponent<SpringJoint>();
						grabbable.Grab(joint, grabOffset);
					}
					else
					{
						grabbable.Grab(transform);
					}
					isGrabbing = true;
				}
				else
				{
					if (useJoint)
					{
						Destroy(joint);
					}
					else
					{
						grabbable.UnGrab();
					}
					isGrabbing = false;
				}
			}
		}
    }

	private void OnTriggerEnter(Collider other)
	{
		if (isGrabbing)
			return;

		Grabbable g = other.GetComponent<Grabbable>();
		if (g)
			grabbable = g;
	}

	private void OnTriggerExit(Collider other)
	{
		if (isGrabbing || !grabbable)
			return;

		if (grabbable.gameObject == other.gameObject)
			grabbable = null;
	}
}
