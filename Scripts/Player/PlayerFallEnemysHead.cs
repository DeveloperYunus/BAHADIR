using UnityEngine;

public class PlayerFallEnemysHead : MonoBehaviour
{
    public PolygonCollider2D colliderr;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            colliderr.isTrigger = false;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            colliderr.isTrigger = true;
    }
}
