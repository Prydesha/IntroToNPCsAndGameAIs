public abstract class Decorator : Node
{
    protected Node _child;
    // Nothing else to do here. Inheriting decorators
    // will implement their own logic to perform
    // when running the child
}
