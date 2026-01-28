public abstract class Composite : Node
{
    protected Node[] _children;
    protected int _childIndex = 0;
    protected bool AllChildrenHaveRun => _childIndex >= _children.Length;
}
