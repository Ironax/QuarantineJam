using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [Flags]
    public enum State 
    { 
        Chill = 0,
        Disrupted,
        Scared,
        Count
    }

    private GameObject player;

    [SerializeField]
    private State state;

    private NavMeshAgent agent;

    private Vector3 targetPos;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        targetPos = player.transform.position;
        agent.destination = targetPos;
    }

    private void Update()
    {
        switch(state)
        {
            case State.Chill:
                ToChill();
                ChillBehaviour();
                break;
            case State.Disrupted:
                ToDisrupted();
                DisruptedBehaviour();
                break;
            case State.Scared:
                ToScared();
                ScaredBehaviour();
                break;
        }
    }

    private void ChillBehaviour()
    {
        // if close to player move towards him, if not approach him
        if (agent.remainingDistance < 0.1f)
        {
            targetPos = player.transform.position;
            agent.destination = targetPos;
        }
    }

    private void DisruptedBehaviour()
    {
        // modifie destination when moved from state or ai will continue to move towards player
        agent.destination = player.transform.position;
    }

    private void ScaredBehaviour()
    {
        // get away from point where became scared
        agent.destination = (transform.position - player.transform.position).normalized * 100.0f;
    }

    private void ToChill()
    {
        agent.speed = 1;
        agent.acceleration = 10;
    }

    private void ToDisrupted()
    {
        agent.speed = 10;
        agent.acceleration = 10;
    }

    private void ToScared()
    {
        agent.speed = 10;
        agent.acceleration = 10;
    }
}
