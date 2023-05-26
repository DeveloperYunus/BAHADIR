using UnityEngine;

public class MCGiveDamage : MonoBehaviour
{
    public PlayerController pc;
    public float SwordInEnemySlow;
    float slowTimer, zaman;

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyOther eo = other.transform.GetComponent<EnemyOther>();
        if (other.CompareTag("Enemy") & eo)  
        {
            slowTimer = 0.6f;
            pc.swordInEnemy = true;
            pc.swordInEnemySlow = SwordInEnemySlow;
            eo.EnemyGetDamage(PlayerController.damage, PlayerController.damageType, transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) 
        {
            pc.swordInEnemy = false;
            pc.swordInEnemySlow = 1;
        }
    }

    private void Update()
    {
        if (Time.time >= zaman && slowTimer > 0) 
        {
            zaman = Time.time + 0.1f;
            slowTimer -= Time.deltaTime;

            if (slowTimer <= 0)
            {
                slowTimer = 0;
                pc.swordInEnemySlow = 1;
                pc.swordInEnemy = false;
            }
        }
    }
}
