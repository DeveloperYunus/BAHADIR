using UnityEngine;
using Spriter2UnityDX;

public class EnemyHPSpriteSetting : MonoBehaviour
{
    public EntityRenderer entityR;

    void Start()
    {
        GetComponent<EntityRenderer>().SortingLayerID = entityR.SortingLayerID;
        GetComponent<EntityRenderer>().SortingOrder = entityR.SortingOrder;
    }
}
