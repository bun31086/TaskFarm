using UnityEngine;

public interface ITargetProduct
{

    ProductState ProductState
    {

        get;

    }

    bool MatchCheck(GameObject collisionObj);
    void SetProductInformation(TargetProductManagerClass targetProductManagerClass, float submissionTimeLimit, ProductState productState);
    void Initialization();

}
