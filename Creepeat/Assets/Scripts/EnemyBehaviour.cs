using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [Flags]
    enum State { Chill = 0x01, Disrupted = 0x02, Scared = 0x04, Count = 3}

    [SerializeField]
    private State state = State.Chill;

    [SerializeField]
    private float disruptedTime = 2.0f;

    [SerializeField]
    private List<GameObject> scaredWaypoints = new List<GameObject>();

    private GameObject player   = null;
    private NavMeshAgent agent  = null;

    private float timer = 0.0f;
    private bool timerStart = false;

    public Vector3 Destination { get => agent.destination; set => agent.destination = value; }

    public void StartTimer()
    {
        timerStart = true;
    }

    private void ResetTimer()
    {
        timer = 0.0f;
        timerStart = false;
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent  = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (agent.enabled == false)
            return;

        agent.destination = player.transform.position;

        switch (state)
        {
            case State.Chill:
                ToChill();
                break;
            case State.Disrupted:
                ToDisrupted();
                break;
            case State.Scared:
                ToScared();
                break;
        }
    }

    private void Update()
    {
        if (timerStart)
            timer += Time.deltaTime;

        if (agent.enabled == false)
            return;

        switch(state)
        {
            case State.Chill:
                ChillBehaviour();
                break;
            case State.Disrupted:
                DisruptedBehaviour();
                break;
            case State.Scared:
                ScaredBehaviour();
                break;
        }

        if (Input.GetKeyDown(KeyCode.S))
            ToScared();
    }

    private void ChillBehaviour()
    {
        if (agent.remainingDistance < 0.5f)
        {
            agent.destination = player.transform.position;
        }
    }

    private void DisruptedBehaviour()
    {
        agent.destination = player.transform.position;

        if (timer > disruptedTime)
            ToChill();
    }

    private void ScaredBehaviour()
    {
        if (agent.remainingDistance < 0.5f)
        {
            ToChill();
        }
    }

    public void ToChill()
    {
        ResetTimer();

        agent.speed        = 1.0f;
        agent.acceleration = 1.0f;

        state = State.Chill;
    }

    public void ToDisrupted()
    {
        ResetTimer();

        agent.speed        = 10.0f;
        agent.acceleration = 5.0f;

        state = State.Disrupted;
    }

    public void ToScared()
    {
        ResetTimer();

        agent.speed        = 10.0f;
        agent.acceleration = 10.0f;

        agent.destination = BestFitWaypoint();

        state = State.Scared;
    }

    private Vector3 BestFitWaypoint()
    {
        Vector3 bestFit = Vector3.zero;

        float angle = -1.0f;

        Vector3 playerToEnemy = (transform.position - player.transform.position).normalized;

        foreach (GameObject waypoint in scaredWaypoints)
        {
            float dot = Vector3.Dot(playerToEnemy, (waypoint.transform.position - transform.position).normalized);

            if (dot > angle)
            {
                angle = dot;
                bestFit = waypoint.transform.position;
            }
        }

        return bestFit;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("kill player here");
        }
    }
}
