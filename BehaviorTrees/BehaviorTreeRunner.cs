using UnityEngine;

public class BehaviorTreeRunner : MonoBehaviour
{
    private Node _rootNode;
    private Node _activeNode;

    void Start()
    {
        _activeNode = _rootNode;  
    }

    void Update()
    {
        RunResult result = _activeNode.Run(this);
        switch (result.Status)
        {
            case NodeStatus.Running:
                // do nothing, let the node keep running
                break;
            case NodeStatus.Success:
            case NodeStatus.Failure:
                // return to the node's parent to determine next steps
                _activeNode = result.ActiveNode.Parent;
                break;
        }
    }
}
