﻿

public class LogicStateMachine<T> where T : class
{
    private T owner_entity;
    private LogicState<T> current_state;

    public void Init(T _owner, LogicState<T> initState)
    {
        owner_entity = _owner;
        ChangeState(initState);
    }
    public void Update()
    {
        if (current_state != null)
        {
            current_state.update(owner_entity);
            return;
        }
    }
    public void ChangeState(LogicState<T> newState)
    {
        if (newState == null) return;
        if (current_state != null)
        {
            current_state.exit(owner_entity);
        }
        current_state = newState;
        current_state.enter(owner_entity);
    }
    public void Set_CurrentState(LogicState<T> state)
    {
        current_state = state;
    }
    public LogicState<T> Get_CurrentState()
    {
        return current_state;
    }
}
