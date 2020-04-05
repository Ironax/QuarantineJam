using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PhoneScenario : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private float timeToWaitBeforePhoneRinging = 5.0f;

    [SerializeField]
    List<GameObject> objectsToDesactivateWhenAmsweringPhone = new List<GameObject>();

    private AudioSource audioSource;
    private bool isPhoneRinging = false;
    private bool hasAnwswered = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (isPhoneRinging && hasAnwswered == false)
            canvas.gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButton("MakeSound") && isPhoneRinging && hasAnwswered == false)
        {
            enemy.GetComponent<NavMeshAgent>().enabled = true;

            audioSource.Stop();

            foreach (GameObject obj in objectsToDesactivateWhenAmsweringPhone)
                obj.SetActive(false);

            hasAnwswered = true;
            canvas.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.gameObject.SetActive(false);
    }

}
