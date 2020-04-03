using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEmitter : MonoBehaviour
{
    [SerializeField]
    private int recordingTime = 5;

    private bool micConnected = false;

    private int minFreq;
    private int maxFreq;

    [HideInInspector]
    public AudioSource playerAudioSource;

    [HideInInspector]
    public bool isRecording = false;

    //Use this for initialization  
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
                Microphone.End(null);
                isRecording = false;
            }

        }
        else
        {
            Debug.LogWarning("Still No Microphone detected!");
        }

    }

}
