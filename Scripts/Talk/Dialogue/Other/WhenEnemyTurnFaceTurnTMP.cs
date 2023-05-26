using UnityEngine;
using TMPro;

public class WhenEnemyTurnFaceTurnTMP : MonoBehaviour
{
    public GameObject motherEnemyObject;
    public TextMeshPro b;
    float value;

    private void Start()
    {
        value = motherEnemyObject.GetComponent<Transform>().localScale.x;
    }

    private void FixedUpdate()
    {
        if (value != motherEnemyObject.GetComponent<Transform>().localScale.x) 
        {
            value = motherEnemyObject.GetComponent<Transform>().localScale.x;
            Vector3 scaler = b.transform.localScale;
            scaler.x *= -1;
            b.transform.localScale = scaler;
        }
    }
}
