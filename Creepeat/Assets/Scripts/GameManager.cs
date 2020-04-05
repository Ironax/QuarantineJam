using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[SerializeField]
	private Canvas canvas;

	public Action onGameOver;

	public void CloseGame()
	{
		Application.Quit();
	}

	private void Awake()
	{
		if (!Instance)
			Instance = this;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			if (Cursor.lockState != CursorLockMode.Locked)
				Cursor.lockState = CursorLockMode.Locked;
			else
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
			}

			canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
		}
	}

	// add function to quitting event //
	public void AddOnClose(Action action)
	{
		Application.quitting += action;
	}

	public void GoToGameOverScene()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		SceneManager.LoadScene(1);
	}

	public void GameOver()
	{
		var camShake = Camera.main.GetComponent<CameraShake>();
		camShake.enabled = true;
		const float delay = 2.0f;
		camShake.shakeDuration = delay;

		onGameOver?.Invoke();
		Invoke("GoToGameOverScene", delay);
	}

	public void LaunchGame()
	{
		SceneManager.LoadScene(0);
	}

	public void WinGame()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		SceneManager.LoadScene(2);
	}

}
