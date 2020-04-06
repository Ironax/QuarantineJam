using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	public void OnUse()
	{
		GameManager.Instance.WinGame();
	}
}
