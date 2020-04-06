using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour
{
	Rigidbody rb;
    [SerializeField]
    bool isFlour = false;
    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
    }

	public void Grab(Joint grabbedBy, Vector3 grabOffset)
	{
		grabbedBy.anchor = grabbedBy.transform.InverseTransformPoint(transform.position + grabOffset);
		grabbedBy.autoConfigureConnectedAnchor = false;
		grabbedBy.connectedAnchor = Vector3.zero;
		grabbedBy.connectedBody = rb;
	}

	public void Grab(Transform grabbedBy)
	{
		rb.isKinematic = true;
		transform.SetParent(grabbedBy);
		//transform.localPosition = offSet;
	}

	public void UnGrab()
	{
		rb.isKinematic = false;
		transform.SetParent(null);
	}

    public void Throw()
    {
        rb.isKinematic = false;
        Vector3 forward = transform.parent.transform.forward;
        transform.SetParent(null);
        rb.AddForce(forward * 1000.0f);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isFlour)
        {
            SplashObject script = collision.gameObject.GetComponent<SplashObject>();
                if (script)
                {
                    script.PaintOn();
                    Destroy(gameObject);
                }
        }
    }
}
