using System.Collections;
using UnityEngine;

public class ChildrenRun : MonoBehaviour
{
    public float WaitT, RunT, RunSpeed;

    bool goRight, facingRight;
    Animator animator;
    Rigidbody2D rb;


    private void Start()
    {
        facingRight = true;
        goRight = true;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(RunChildren("kos", Random.Range(WaitT - 1, WaitT + 1), Random.Range(RunT - 1.5f, RunT + 1.5f)));
    }

    void TurnFace()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    IEnumerator RunChildren(string a, float waitT, float runT)
    {
        switch (a)
        {
            case "kos":
                yield return new WaitForSeconds(waitT);
                if (goRight)
                {
                    rb.velocity = new Vector2(Random.Range(RunSpeed - 0.15f, RunSpeed + 0.15f), 0);
                    animator.SetFloat("speedx", Mathf.Abs(rb.velocity.x));
                    if (!facingRight) TurnFace();
                    goRight = !goRight;
                }
                else
                {
                    rb.velocity = new Vector2(Random.Range(-RunSpeed - 0.15f, -RunSpeed + 0.15f), 0);
                    animator.SetFloat("speedx", Mathf.Abs(rb.velocity.x));
                    if (facingRight) TurnFace();
                    goRight = !goRight;
                }
                StartCoroutine(RunChildren("bekle", Random.Range(WaitT - 1, WaitT + 1), Random.Range(RunT - 1.5f, RunT + 1.5f)));
                break;

            case "bekle":
                yield return new WaitForSeconds(runT);
                rb.velocity = new Vector2(0, 0);
                animator.SetFloat("speedx", Mathf.Abs(rb.velocity.x));

                StartCoroutine(RunChildren("kos", Random.Range(WaitT - 1, WaitT + 1), Random.Range(RunT - 1.5f, RunT + 1.5f)));
                break;

        }
    }

    public void RunChildrenForOut()
    {
        StartCoroutine(RunChildren("kos", Random.Range(WaitT - 1, WaitT + 1), Random.Range(RunT - 1.5f, RunT + 1.5f)));
    }
}
