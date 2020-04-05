﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager Instance { get { return _instance; } }

    AudioClip tmpClip;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    [SerializeField]
    GameObject player;

    List<GameObject> enemyList = new List<GameObject>();

    PlayerSoundEmitter playerSoundEmmiter;
    AudioSource playerAudioSource;

    AudioSource audioSource;

    void Start()
    {
        playerSoundEmmiter = player.GetComponent<PlayerSoundEmitter>();
        playerAudioSource = player.GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();

        GameObject[] objects = FindObjectsOfType<GameObject>();
      
        foreach (GameObject go in objects)
        {
            if (go.tag == "Enemy")
                enemyList.Add(go);
        }
    }

    void Update()
    {
        if (playerSoundEmmiter.isRecording == false && playerAudioSource.clip != null && playerAudioSource.clip.length > 0)
        {
            foreach (GameObject enemy in enemyList)
            {
                enemy.GetComponent<EnemySoundManagement>().hasBeenPlayed = false;
                enemy.GetComponent<EnemySoundManagement>().timeToWait = Vector3.Distance(enemy.transform.position, player.transform.position) * 0.035f;
                enemy.GetComponent<AudioSource>().clip = playerAudioSource.clip;
            }

            playerAudioSource.clip = null;
        }

        foreach (GameObject enemy in enemyList)
        {
            if (enemy.GetComponent<EnemyBehaviour>().state == EnemyBehaviour.State.Disrupted)
            {
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
        }
    }

    public void UpdateGeneralSound(float value)
    {
        AudioListener.volume = value;
    }
}
