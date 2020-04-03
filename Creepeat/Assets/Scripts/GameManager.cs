using System;
using UnityEngine;

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
        if (Input.GetKeyDown(KeyCode.Escape))
            canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
    }

    // add function to quitting event //
    public void AddOnClose(Action action)
    {
        Application.quitting += action;
    }
}
