class Inverter : Decorator
{
    public override RunResult Run(BehaviorTreeRunner runner)
    {
        RunResult childResult = _child.Run(runner);
        switch (childResult.Status)
        {
            default:
            case NodeStatus.Running:
                return childResult;
            case NodeStatus.Success:
                return new RunResult
                {
                    ActiveNode = this,
                    Status = NodeStatus.Failure
                };
            case NodeStatus.Failure:
                return new RunResult
                {
                    ActiveNode = this,
                    Status = NodeStatus.Success
                };
        }
    }
}
