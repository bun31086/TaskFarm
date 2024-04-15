using UnityEngine;

public interface ITargetProduct
{

    float SubmissionTimeLimit
    {

        get;
    
    }
    ProductState ProductState
    {

        get;

    }

    bool MatchCheck(GameObject collisionObj);
    void SetProductInformation(TargetProductManagerClass targetProductManagerClass, float submissionTimeLimit, ProductState productState);
    void Initialization();

}
