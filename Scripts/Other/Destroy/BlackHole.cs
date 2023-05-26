using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public string description = "Icine giren herseyi Destroy eder.";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
