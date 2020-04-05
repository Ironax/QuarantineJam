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

    public AudioClip ScreamClip;

    [SerializeField]
    private int nbrOfAudioPossible = 20;

    private AudioSource goAudioSource;

    private List<AudioClip> listOfAllAudios = new List<AudioClip>();

    private float timeBeetweenRandomSound = 0f;
    private float randomTimeBeetweenSounds = 0f;

    void Start()
    {
        randomTimeBeetweenSounds = Random.Range(10f, 15f);
        timeBeetweenRandomSound = randomTimeBeetweenSounds;
        goAudioSource = GetComponent<AudioSource>();
        GameManager.Instance.onGameOver += Scream;
    }

    void Update()
    {
        if (!hasBeenPlayed && goAudioSource.clip != null)
        {
            PlaySoundIfItExist();
            timeBeetweenRandomSound = randomTimeBeetweenSounds;
        }
        else
        {
            PlayRandomSound();
        }
    }

    private void PlaySoundIfItExist()
    {
        timeToWait -= Time.deltaTime;

        if (timeToWait <= 0.0f)
        {
            if (goAudioSource.clip != null && !goAudioSource.isPlaying)
            {
                if (!listOfAllAudios.Contains(goAudioSource.clip))
                {
                    if (listOfAllAudios.Count >= nbrOfAudioPossible)
                        listOfAllAudios.Remove(listOfAllAudios[0]);

                    listOfAllAudios.Add(goAudioSource.clip);
                }

                goAudioSource.Play();
                hasBeenPlayed = true;
            }
        }
    }

    private void PlayRandomSound()
    {
        if (listOfAllAudios.Count <= 0)
            return;

        timeBeetweenRandomSound -= Time.deltaTime;

        if (timeBeetweenRandomSound <= 0)
        {
            int random = Random.Range(0, listOfAllAudios.Count);

            goAudioSource.clip = listOfAllAudios[random];

            goAudioSource.Play();

            timeBeetweenRandomSound = randomTimeBeetweenSounds;
        }
    }

    private void Scream()
    {
	    goAudioSource.pitch = 1.0f;
		goAudioSource.clip = ScreamClip;
		goAudioSource.Play();
    }

}
