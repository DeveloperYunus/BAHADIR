using UnityEngine;
using TMPro;
using Spriter2UnityDX;

public class EnemyHPLayerShower : MonoBehaviour
{
    TextMeshPro enemyHPAmount;

    private void Start()
    {
        enemyHPAmount = GetComponent<TextMeshPro>();
        Invoke(nameof(HPLayerShower), 0.5f);
    }

    public void HPLayerShower()
    {
        enemyHPAmount.sortingOrder = transform.parent.GetComponent<EntityRenderer>().SortingOrder + 1;
        enemyHPAmount.sortingLayerID = transform.parent.GetComponent<EntityRenderer>().SortingLayerID;
    }
}
