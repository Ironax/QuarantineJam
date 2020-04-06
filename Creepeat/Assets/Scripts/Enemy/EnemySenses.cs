using UnityEngine;

public class EnemySenses : MonoBehaviour
{
    private EnemyBehaviour behaviour        = null;
    private SphereCollider disruptionSphere = null;
    
    private void Awake()
    {
        behaviour        = GetComponentInParent<EnemyBehaviour>();
        disruptionSphere = GetComponent<SphereCollider>();
    }
    
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            behaviour.ToDisrupted();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            behaviour.StartTimer();
        }
    }
}
