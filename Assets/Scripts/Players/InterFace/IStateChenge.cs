interface IstateChenge
{

    void ChangeMoveState(IMoveState nextState);
    void ChangeBehaviorState(IBehaviourState nextState);

    void Update();

}