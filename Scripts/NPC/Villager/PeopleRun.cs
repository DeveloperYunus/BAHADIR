using System.Collections;
using UnityEngine;

public class PeopleRun : MonoBehaviour
{
    public float speed, startRunTime;
    public bool turnFace, erkek, isTalk;
    public Collider2D zerlikTalkCollider;

    public static bool run;
    bool isDead, oneTime;
    AudioManager audioManager;
    Animator animator;
    Rigidbody2D rb;


    private void Start()
    {
        oneTime = true;
        isDead = false;
        run = false;
        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (run && !isDead && oneTime)
        {
            oneTime = false;
            zerlikTalkCollider.enabled = true;
            StartCoroutine(StartRun(1f));
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.CompareTag("Bullet") && Vector2.Distance(other.transform.position, transform.position) < 1.5f)
        {
            PlayerPrefs.SetString("level3plyrkatlettimi","evet");
            if (!run)
            {
                run = true;
                audioManager.playSound("shortThriller");
            }
            isDead = true;

            if (erkek)
            {
                int a = Random.Range(0, 2);
                if (a == 0) audioManager.playSound("manScream1"); 
                else audioManager.playSound("manScream2"); 
            }
            else
            {
                int a = Random.Range(0, 2);
                if (a == 0) audioManager.playSound("womanScream2");
                else audioManager.playSound("womanScream3");
            }

            GetComponents<BoxCollider2D>()[0].enabled = false;
            if(isTalk) GetComponents<BoxCollider2D>()[1].enabled = false;
            rb.velocity = new Vector2(0, 0);

            if (PlayerController.damageType == 1)
            {
                int a = Random.Range(0, 2);
                if (a == 0) audioManager.playSound("cut1");
                else audioManager.playSound("cut2");
            }
            animator.Play("Dead");
        }
    }

    IEnumerator StartRun(float time)
    {
        yield return new WaitForSeconds(time);
        if(!isDead) rb.velocity = new Vector2(-Random.Range(speed + 0.4f, speed - 0.4f), 0);
        animator.SetFloat("speedx", 1);
        if (turnFace)
        {
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }
}
