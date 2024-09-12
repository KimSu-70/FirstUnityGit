using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class NavMove : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;

    [SerializeField] Transform target;

    private void Start()
    {
        // agent.destination = new Vector3(0, 0, 1);       //목적지 이동
        // agent.SetDestination(target.position);
        // agent.destination = target.position;
        StartCoroutine(MoveRoutine());
        
    }

    IEnumerator MoveRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(0.2f);

        while (true)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            agent.destination = target.position;
            yield return delay;
        }
    }
}
