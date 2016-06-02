

public class CardStateMachine<T> where T : class
{
    private T owner_entity;
    private CardState<T> current_state;

    public void Init(T _owner, CardState<T> initState)
    {
        owner_entity = _owner;
        ChangeState(initState);
    }
    public void Update()
    {
        if(current_state != null)
        {
            current_state.update(owner_entity);
            return;
        }
    }
    public void MouseDown()
    {
        current_state.mouseDown(owner_entity);
    }
    public void MouseUp()
    {
        current_state.mouseUp(owner_entity);
    }
    public void MouseDrag()
    {
        current_state.mouseDrag(owner_entity);
    }
    public void ChangeState(CardState<T> newState)
    {
        if (newState == null) return;
        if(current_state != null)
        {
            current_state.exit(owner_entity);
        }
        current_state = newState;
        current_state.enter(owner_entity);
    }
    public void Set_CurrentState(CardState<T> state)
    {
        current_state = state;
    }
    public CardState<T> Get_CurrentState()
    {
        return current_state;
    }
}
