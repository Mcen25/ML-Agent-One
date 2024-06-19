using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgentShooter : Agent
{

    public Transform shootingPoint;
    public int minStepsBetweenShots = 50;
    public int damage = 100;

    private bool ShotAvailable = true;
    private int stepsUntilShotIsAvailable = 0;

    private Vector3 StartingPosition;
    private Rigidbody Rb;
    public override void OnEpisodeBegin()
    {
        Debug.Log("Episode Begin");
        transform.position = StartingPosition;
        Rb.velocity = Vector3.zero;
        ShotAvailable = true;
    }

    public override void Initialize()
    {
        StartingPosition = transform.position;
        Rb = GetComponent<Rigidbody>();
    }

    // public override void CollectObservations(VectorSensor sensor)
    // {
        
    // }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (Mathf.RoundToInt(actions.ContinuousActions[0]) >= 1) {
            Shoot();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continousActions = actionsOut.ContinuousActions;
        continousActions[0] = Input.GetKey(KeyCode.F) ? 1f : 0f;
        
    }

    private void FixedUpdate() {
        if (!ShotAvailable) {
            stepsUntilShotIsAvailable--;

            if (stepsUntilShotIsAvailable <= 0) {
                ShotAvailable = true;
            }
        }
    }

    private void Shoot() {
        if (!ShotAvailable) {
                return;
        }

        var layerMask = 1 << LayerMask.NameToLayer("Enemy");
        var direction = transform.forward;

        Debug.Log("Shot");
        Debug.DrawRay(shootingPoint.position, direction * 200f, Color.green, 2f);

        if (Physics.Raycast(shootingPoint.position, direction, out var hit, 200f, layerMask)) {
                hit.transform.GetComponent<Target>().ApplyDamage(damage);
        }

        ShotAvailable = false;
        stepsUntilShotIsAvailable = minStepsBetweenShots;
    }

    public void RegisterKill() {
        AddReward(1f);
        EndEpisode();
    }
} 
