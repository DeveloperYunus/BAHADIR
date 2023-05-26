using UnityEngine;

public class FlowerBomb : MonoBehaviour
{
    public ParticleSystem poisonBomb;
    int bombDamage;
    float bombDistance;
    AudioManager au;
    GameObject player;
    Animator a;

    private void Start()
    {
        au = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        player = GameObject.Find("Player");
        a = GetComponent<Animator>();
        bombDamage = GetComponentInParent<PoisonFlower>().bombDamageAmount;
        bombDistance = GetComponentInParent<PoisonFlower>().bombDistance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            a.Play("poisonFlower");
            poisonBomb.Play(false);
            au.playSound("poisonBomb");

            if (Vector2.SqrMagnitude(player.transform.position - gameObject.transform.position) < bombDistance)
            {
                player.GetComponent<PlayerController>().PlayerGetDamage(bombDamage, 7);
            }
        }
    }
}
