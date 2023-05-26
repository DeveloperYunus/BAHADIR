using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public GameObject explosionEffect;
    public GameObject stayIceBall;
    public Rigidbody2D rb;

    [HideInInspector] public int damageType;
    float damage;
    AudioManager audioManager;

    void Start()
    {
        damage = PlayerController.damage;
        damageType = PlayerController.damageType;
        rb.velocity = transform.right * speed;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        int a = Random.Range(0, 2);
        if (damageType == 2)
        {
            if (a == 0)
                audioManager.playSound("fireGo1");
            else if (a == 1)
                audioManager.playSound("fireGo2");
        }
        else if (damageType == 3)
        {
            audioManager.playSound("iceGo1");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyOther enemy = other.GetComponent<EnemyOther>();
        if (other.CompareTag("Ground") | other.CompareTag("Enemy") | other.CompareTag("Villager") | other.CompareTag("Bullet"))
        {
            if (other.offset.x >= 1f)// eger offset 1 den buyukse bu muhtemelen bir konusma colliderýdýr 
            {
                return;
            }
            Triggered();
            if (enemy) 
            {
                enemy.EnemyGetDamage(damage, damageType, transform);
            }
            PlayerBulletProcedur(other.gameObject, true); 
        }
    }

    public void Triggered()
    {
        int a = Random.Range(0, 2);
        if (damageType == 2)
        {
            if (a == 0)
                audioManager.playSound("fireExp1");
            else if (a == 1)
                audioManager.playSound("fireExp2");
        }
        else if (damageType == 3)
        {
            if (a == 0)
                audioManager.playSound("iceExp1");
            else if (a == 1)
                audioManager.playSound("iceExp2");
        }

        GetComponent<CircleCollider2D>().enabled = false;
        rb.velocity = transform.right * 3;
        GetComponent<Transform>().DOScale(0, 0.3f);
    }
    public void PlayerBulletProcedur(GameObject other,bool iceBallGereklimi)
    {
        if (iceBallGereklimi && damageType == 3) //buztopu demektir
        {
            Instantiate(stayIceBall, transform.position, transform.rotation, other.transform);
        }

        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject, 0.3f);
    }
}
