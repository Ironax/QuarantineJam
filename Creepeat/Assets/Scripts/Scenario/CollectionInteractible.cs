using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionInteractible : MonoBehaviour
{
	[SerializeField]
	int collectiblesRequired = 1;

    public void TryUse(int collectibles)
	{
		if(collectibles >= collectiblesRequired)
		{
			gameObject.SendMessage("OnUse");
		}
	}
}
