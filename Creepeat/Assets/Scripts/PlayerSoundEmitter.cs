using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEmitter : MonoBehaviour
{
    [SerializeField]
    private int recordingTime = 5;

    [HideInInspector]
    public AudioSource playerAudioSource;

    [HideInInspector]
    public bool isRecording = false;

    private bool micConnected = false;

    private int minFreq;
    private int maxFreq;

    private float recordingOffsetSeconds = 0.5f;

    void Start()
    {
        InitMicrophone();
    }

    private void Update()
    {
        CheckForSounds();
    }

    private void InitMicrophone()
    {
        playerAudioSource = GetComponent<AudioSource>();
             
        if (Microphone.devices.Length <= 0)
        {
            Debug.LogWarning("Microphone not connected!");
            micConnected = false;
        }
        else
        {
            micConnected = true;

            Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);

            if (minFreq == 0 && maxFreq == 0)
            {
                maxFreq = 44100;
            }
        }
    }

    private void CheckForSounds()
    {
        if (micConnected)
        {
            if (Input.GetButton("MakeSound"))
            {
                if (!Microphone.IsRecording(null))
                {
                    isRecording = true;
                    playerAudioSource.clip = Microphone.Start(null, false, recordingTime, maxFreq);
                }
            }
            else
            {
                recordingOffsetSeconds -= Time.deltaTime;

                if (recordingOffsetSeconds <= 0.0f)
                {
                    Microphone.End(null);
                    isRecording = false;
                    recordingOffsetSeconds = 0.5f;
                }
            }

        }
        else
        {
            Debug.LogWarning("Still No Microphone detected!");
        }

    }

}
