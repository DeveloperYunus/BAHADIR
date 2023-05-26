using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    [HideInInspector]
    public float damage;

    //yukarý cýkma iþlevi bitince bunu kaydeder cunku yukarý cýkarken yakalanýrsa daha cok hasar alýr.
    [HideInInspector]
    public bool IsDownRise;
    public static float playerAnimatorSpeed, playerNewSpeed;
    PlayerController PC;

    public AudioSource triggered1;
    public AudioSource triggered2;
    public AudioSource slash1, slash2;
    public AudioSource pikeUp1, pikeUp2;

    private void Start()
    {
        PC = GameObject.Find("Player").GetComponent<PlayerController>();
        IsDownRise = false;
        playerAnimatorSpeed = 1;
        playerNewSpeed = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int a = Random.Range(0, 2);
        if (collision.CompareTag("Player"))
        {
            if (!IsDownRise)
            {
                playerNewSpeed = 0.4f;
                PC.PlayerGetDamage(damage, 4);
                playerAnimatorSpeed = 0.5f;
                if (a == 0)
                    slash1.Play();
                else
                    slash2.Play();

            }
            else if (collision.GetComponent<Rigidbody2D>().velocity.y < -0.05f)
            {
                playerNewSpeed = 0.4f;
                PC.PlayerGetDamage(damage * 0.65f, 4);
                if (a == 0)
                    slash1.Play();
                else
                    slash2.Play();
                playerAnimatorSpeed = 0.5f;
            }
            else
            {
                playerNewSpeed = 0.4f;
                playerAnimatorSpeed = 0.5f;
            }//player
        }
        else if (collision.CompareTag("Enemy"))
        {
            if (!IsDownRise)
            {
                if (collision.GetComponent<EnemyAII>())             collision.GetComponent<EnemyAII>().enemyNewSpeed = 0.77f;
                else if (collision.GetComponent<EnemyRangedAII>())  collision.GetComponent<EnemyRangedAII>().enemyNewSpeed = 0.77f;
                if (a == 0) slash1.Play();
                else slash2.Play();

                collision.GetComponent<EnemyOther>().EnemyGetDamage(damage, 4, transform);
                collision.GetComponent<EnemyOther>().enemyAnimatorSpeed = 0.5f;
            }
            else if (collision.GetComponent<Rigidbody2D>().velocity.y < -0.05f)
            {
                if (collision.GetComponent<EnemyAII>())             collision.GetComponent<EnemyAII>().enemyNewSpeed = 0.77f;
                else if (collision.GetComponent<EnemyRangedAII>())  collision.GetComponent<EnemyRangedAII>().enemyNewSpeed = 0.77f;
                if (a == 0) slash1.Play();
                else slash2.Play();

                collision.GetComponent<EnemyOther>().EnemyGetDamage(damage * 0.65f, 4, transform);
                collision.GetComponent<EnemyOther>().enemyAnimatorSpeed = 0.5f;
            }
            else
            {
                if (collision.GetComponent<EnemyAII>())             collision.GetComponent<EnemyAII>().enemyNewSpeed = 0.77f;
                else if (collision.GetComponent<EnemyRangedAII>())  collision.GetComponent<EnemyRangedAII>().enemyNewSpeed = 0.77f;
                collision.GetComponent<EnemyOther>().enemyAnimatorSpeed = 0.5f;
            }//enemy
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerNewSpeed = 1f;
            playerAnimatorSpeed = 1f;
        }
        else if(collision.CompareTag("Enemy"))
        {
            if(collision.GetComponent<EnemyAII>())              collision.GetComponent<EnemyAII>().enemyNewSpeed = 1;
            else if (collision.GetComponent<EnemyRangedAII>())  collision.GetComponent<EnemyRangedAII>().enemyNewSpeed = 1;

            collision.GetComponent<EnemyOther>().enemyAnimatorSpeed = 1;
        }
    }
}