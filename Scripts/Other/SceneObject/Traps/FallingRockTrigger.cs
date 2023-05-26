using UnityEngine;

public class FallingRockTrigger : MonoBehaviour
{
    public FallingRockTrap FRTrap;
    public string whichCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (whichCollider == "ForBullet" && collision.CompareTag("Bullet"))
        {
            FRTrap.RocksStartFalll();
        }
        else if(whichCollider == "ForBody" && collision.CompareTag("Player") || whichCollider == "ForBody" && collision.CompareTag("Enemy"))
        {
            FRTrap.RocksStartFalll();
        }
    }
}
