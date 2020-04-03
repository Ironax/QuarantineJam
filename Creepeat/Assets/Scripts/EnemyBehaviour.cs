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

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private State state;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
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
    }

    private void ChillBehaviour()
    { 
        // if close to payer move towards him, if not approach him
    }

    private void DisruptedBehaviour()
    {
        // modifie destination when moved from state or ai will continue to move towards player
        agent.destination = player.transform.position;
    }

    private void ScaredBehaviour()
    { 
        // get away from point where became scared
    }
}
