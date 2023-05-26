using System.Collections;
using UnityEngine;
using DG.Tweening;

public class StoneForRockTrap : MonoBehaviour
{
    public bool isGiveDamage;
    public float damage;
    
    bool oneTime;
    float zaman;

    [Header("Sounds")]
    public AudioSource toGround1;
    public AudioSource toGround2, toGround3;
    public AudioSource toAlive1, toAlive2, toAlive3;
    [HideInInspector] public float liveTime;

    private void Start()
    {
        StartCoroutine(DieTimer());
    }
    private void Update()
    {
        if (Time.time >= zaman && GetComponent<Rigidbody2D>().velocity.y < -10) 
        {
            oneTime = true;
            isGiveDamage = true;
            gameObject.layer = 0;
            zaman = Time.time + 0.1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isGiveDamage)
        {
            int a = Random.Range(0, 3);
            isGiveDamage = false;
            if(collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerController>().PlayerGetDamage(damage, 5);
                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(10, -10), 0));
                if (a == 0) toAlive1.Play();
                else if (a == 1) toAlive2.Play();
                else toAlive3.Play();
            }
            else if(collision.CompareTag("Enemy"))
            {
                collision.GetComponent<EnemyOther>().EnemyGetDamage(damage, 5, transform);
                if (a == 0) toAlive1.Play();
                else if (a == 1) toAlive2.Play();
                else toAlive3.Play();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (oneTime && collision.CompareTag("Ground"))
        {
            oneTime = false;
            isGiveDamage = false;
            gameObject.layer = 13;

            int a = Random.Range(0, 3);
            if (a == 0) toGround1.Play();
            else if (a == 1) toGround2.Play();
            else toGround3.Play();
        }
    }

    IEnumerator DieTimer()
    {
        yield return new WaitForSeconds(liveTime + Random.Range(1.5f, -1.5f));
        GetComponent<SpriteRenderer>().DOFade(0, 2f);
        yield return new WaitForSeconds(2.1f);
        Destroy(gameObject);
    }
}
