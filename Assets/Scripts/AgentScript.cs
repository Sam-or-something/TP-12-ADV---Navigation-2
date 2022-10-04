using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform [] baseTransforms;
    [SerializeField] Transform targetTransform;
    [SerializeField] float arrivingDistance;
    public bool chaseModeOn = false;
    public int currentBase = 0;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = baseTransforms[0].position;
    }
    
    void Update()
    {
        Debug.Log(agent.remainingDistance);
        if (chaseModeOn)
        {
            agent.destination = targetTransform.position;
        }
        else
        {
            if(agent.remainingDistance < arrivingDistance && !agent.pathPending)
            {
                currentBase++;
                if(currentBase >= baseTransforms.Length)
                {
                    currentBase = 0;
                }
                agent.destination = baseTransforms[currentBase].position;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            chaseModeOn = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            chaseModeOn = false;
        }
    }
}
