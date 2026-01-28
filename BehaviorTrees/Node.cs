public enum NodeStatus
{
    Success,
    Failure,
    Running
}

public struct RunResult
{
    public Node ActiveNode;
    public NodeStatus Status;
}

public abstract class Node
{
    public string Name;
    public Node Parent;

    public abstract RunResult Run(BehaviorTreeRunner runner);
}
