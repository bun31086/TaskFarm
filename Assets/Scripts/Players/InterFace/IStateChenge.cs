using UnityEngine;
interface IstateChenge
{

    void ChangeMoveState(IMoveState nextState,Vector3 moveVector);
    void ChangeBehaviorState(IBehaviourState nextState);

    void Update(float speed);

}