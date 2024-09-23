using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerContorller : MonoBehaviour
{
    [SerializeField] NavMeshAgent player;
    [SerializeField] LayerMask layerMask;

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            Navi(hit);
        }
    }

    private void Navi(RaycastHit hit)
    {
        if(NavMesh.SamplePosition(hit.point, out NavMeshHit meshHit, 0.1f, NavMesh.AllAreas))
        {
            player.SetDestination(meshHit.position);
        }
    }
}
