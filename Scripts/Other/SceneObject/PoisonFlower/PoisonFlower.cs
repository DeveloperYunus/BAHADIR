using UnityEngine;

public class PoisonFlower : MonoBehaviour
{
    public int damageAmount, bombDamageAmount, bombDistance;
    AudioManager audioManager;
    PlayerController pc;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            pc = collision.GetComponent<PlayerController>();
            InvokeRepeating(nameof(PoisonFlowerr), 0, 1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CancelInvoke("PoisonFlowerr");
        }
    }

    void PoisonFlowerr()
    {
        if (!PlayerController.isDead && !pc.protectShield)
        {
            pc.PlayerGetDamage(damageAmount, 6);

            int a = Random.Range(0, 2);
            if (a == 0) audioManager.playSound("poisonDamage1");
            else audioManager.playSound("poisonDamage2");
        }
    }
}
