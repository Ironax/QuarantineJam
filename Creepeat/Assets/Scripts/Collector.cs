using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
	public int collected = 0;

	GameObject collictibleInRange = null;
	CollectionInteractible interactible = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetButtonDown("Use"))
		{
			if(collictibleInRange)
			{
				Destroy(collictibleInRange);
				collected++;
			}
			if(interactible)
			{
				interactible.TryUse(collected);
			}
		}
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Collectible")
		{
			collictibleInRange = other.gameObject;
		}
		else if(other.tag == "Interactible")
		{
			interactible = other.GetComponent<CollectionInteractible>();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (collictibleInRange)
			if (collictibleInRange == other.gameObject)
				collictibleInRange = null;

		if (other.tag == "Interactible")
		{
			interactible = null;
		}
	}
}
