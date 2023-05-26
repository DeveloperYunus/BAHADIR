using UnityEngine;

public class DamageTextForce : MonoBehaviour
{
    public float upForce;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, upForce);
    }
}
