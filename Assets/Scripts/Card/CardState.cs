public abstract class CardState<T> where T : class
{
    public string state_name;

    public abstract void enter(T entity);
    public abstract void update(T entity);
    public abstract void exit(T entity);

    public abstract void mouseUp(T entity);
    public abstract void mouseDown(T entity);
    public abstract void mouseDrag(T entity);
    public string GetStateName() { return state_name; }
}