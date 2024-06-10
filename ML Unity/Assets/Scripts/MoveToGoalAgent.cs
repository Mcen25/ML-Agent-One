using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer floorMeshRenderer;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0, 0.5f, 0);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        float moveY = actions.DiscreteActions[1];
        float grab = actions.DiscreteActions[0];

        transform.localPosition += new Vector3(moveX, moveY, moveZ) * Time.deltaTime * 2.5f;
        // if () {

        // }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");
        discreteActions[0] = Input.GetKey(KeyCode.E) ? 1 : 0;
        discreteActions[1] = Input.GetKey(KeyCode.Space) ? 1 : 0;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal)) {
            SetReward(+1f);
            floorMeshRenderer.material = winMaterial;
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall)) {
            SetReward(-1f);
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
        }
        
    }
}
