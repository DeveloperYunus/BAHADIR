using UnityEngine;
using DG.Tweening;

public class EnemyBullet : MonoBehaviour
{
    public GameObject stayIceBall, explosionEffect;
    [HideInInspector] public float damageType;
    [HideInInspector] public float damage;
    [HideInInspector] public Vector2 direction;
    
    AudioManager audioManager;
    bool onlyOneTime;                                           //sadece bir kez hasar vermesi için

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        onlyOneTime = true;

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
        PlayerController pc = other.GetComponent<PlayerController>();
        if(onlyOneTime && (other.CompareTag("Player") | other.CompareTag ("Ground") | other.CompareTag("Bullet")))
        {
            onlyOneTime = false;
            Triggered();
            if (pc)
            {
                pc.PlayerGetDamage(damage, damageType);
            }

            if (damageType == 3) //buztopu demektir
            {
                Instantiate(stayIceBall, transform.position, transform.rotation, other.transform);
            }         
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

        GetComponent<Rigidbody2D>().velocity = direction * 3;
        GetComponent<Transform>().DOScale(0, 0.3f);

        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject, 0.3f);
    }
}