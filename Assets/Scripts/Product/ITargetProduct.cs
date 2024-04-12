using UnityEngine;

public interface ITargetProduct
{

    bool MatchCheck(GameObject collisionObj);
    void SetProductInformation(TargetProductManagerClass targetProductManagerClass, float submissionTimeLimit, ProductState productState);

}
