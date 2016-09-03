public abstract class CardState<T> where T : class
{
    public abstract void enter(T entity);
    public abstract void update(T entity);
    public abstract void exit(T entity);

    public abstract string GetStateName();
}