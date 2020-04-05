using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PhoneScenario : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    List<GameObject> objectsToDesactivateWhenAmsweringPhone = new List<GameObject>();

    [SerializeField]
    private float timeToWaitBeforePhoneRinging = 5.0f;

    private AudioSource audioSource;
    private bool isPhoneRinging = false;

    void Start()
    {
        enemy.GetComponent<NavMeshAgent>().enabled = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isPhoneRinging)
            WaitBeforePhoneRing();



    }

    private void WaitBeforePhoneRing()
    {
        timeToWaitBeforePhoneRinging -= Time.deltaTime;

        if (timeToWaitBeforePhoneRinging <= 0)
        {
            audioSource.Play();
            isPhoneRinging = true;
        }
    }

}
