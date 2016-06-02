
public abstract class LogicState<T> where T : class
{
    public string state_name;

    public abstract void enter(T entity);
    public abstract void update(T entity);
    public abstract void exit(T entity);

    public string GetStateName() { return state_name; }
}