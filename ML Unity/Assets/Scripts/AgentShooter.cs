using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgentShooter : Agent
{

    public Transform shootingPoint;
    public override void OnEpisodeBegin()
    {
        
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        base.Heuristic(actionsOut);
    }

    private void Shoot(Vector3 direction) {
        int layerMask = 1 << LayerMask.NameToLayer("Target");

        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hit, Mathf.Infinity, layerMask)) {
            Target target = hit.collider.GetComponent<Target>();
            if (target != null) {
                target.TakeDamage(10);
            }
        }
    }
} 
