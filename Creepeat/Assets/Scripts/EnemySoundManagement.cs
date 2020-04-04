using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class EnemySoundManagement : MonoBehaviour
{
    [HideInInspector]
    public float timeToWait = 0f;

    [HideInInspector]
    public bool hasBeenPlayed = false;

    private AudioSource goAudioSource;

    void Start()
    {
        goAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!hasBeenPlayed && goAudioSource.clip != null)
        {
            PlaySoundIfItExist();
        }
    }

    private void PlaySoundIfItExist()
    {
        timeToWait -= Time.deltaTime;

        if (timeToWait <= 0.0f)
        {
            if (goAudioSource.clip != null && !goAudioSource.isPlaying)
            {
                goAudioSource.Play();
                hasBeenPlayed = true;
            }
        }
    }

    IEnumerator PlaySoundCouroutine()
    {
        yield return new WaitForSeconds(timeToWait);

        if (goAudioSource.clip != null && !goAudioSource.isPlaying)
        {
            goAudioSource.Play();
            hasBeenPlayed = true;
        }
    }
}
