public class Sequence : Composite
{
    public override RunResult Run(BehaviorTreeRunner runner)
    {
        if (this.AllChildrenHaveRun)
        {
            // reset from previous run
            _childIndex = 0;
        }
        RunResult currentChildResult = _children[_childIndex].Run(runner);
        switch (currentChildResult.Status)
        {
            case NodeStatus.Running:
                return currentChildResult;
            case NodeStatus.Success:
                _childIndex++;
                return new RunResult
                {
                    ActiveNode = this,
                    Status = AllChildrenHaveRun ? 
                        NodeStatus.Success : NodeStatus.Running
                };
            default:
            case NodeStatus.Failure:
                return new RunResult
                {
                    ActiveNode = this,
                    Status = NodeStatus.Failure
                };

        }
    }
}
