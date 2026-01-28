using UnityEngine;
class MoveLeaf : Leaf
{
    public Vector3 MoveDestination;
    public float Speed;

    public override RunResult Run(BehaviorTreeRunner runner)
    {
        Vector3 direction = MoveDestination - runner.transform.position;
        float distanceToDestination = direction.magnitude;
        NodeStatus returningStatus = NodeStatus.Running;
        if (distanceToDestination < 1f)
        {
            // We're close enough!
            returningStatus = NodeStatus.Success;
        }
        else // We're still running...
        {
            runner.transform.Translate(direction.normalized
                * Mathf.Min(Speed, direction.magnitude));
        }
        return new RunResult
        {
            ActiveNode = this,
            Status = returningStatus
        };
    }
}
