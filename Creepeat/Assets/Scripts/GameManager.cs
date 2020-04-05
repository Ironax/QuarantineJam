using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private Canvas canvas;

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

    public void GameOver()
    {
        SceneManager.LoadScene(1);
    }

    public void LaunchGame()
    {
        SceneManager.LoadScene(0);
    }

}
