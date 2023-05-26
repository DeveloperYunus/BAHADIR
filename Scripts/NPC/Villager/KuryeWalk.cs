using System.Collections;
using UnityEngine;

public class KuryeWalk : MonoBehaviour
{
    public float speed;
    public BoxCollider2D colliderr;

    Rigidbody2D rb;
    Animator a;
    bool ShouldIWalk;
    float zaman;

    private void OnEnable()
    {
        ShouldIWalk = true;

        a = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Stop")
        {
            ShouldIWalk = false;
            StartCoroutine(Colliderrr());
        }        
    }

    private void Update()
    {
        if (Time.time >= zaman)
        {
            if (ShouldIWalk)
                rb.velocity = new Vector2(speed, rb.velocity.y);

            a.SetFloat("speedx", Mathf.Abs(rb.velocity.x));
            zaman = Time.time + 0.1f;
        }
    }

    IEnumerator Colliderrr()
    {
        yield return new WaitForSeconds(15f);
        gameObject.layer = 0;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        colliderr.enabled = true;
    }
}
