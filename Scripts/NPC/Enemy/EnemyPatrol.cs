using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public float distance;
    private bool movingRight = true;
    public Transform groundDetection;
    public Transform groundDetectionH;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb.velocity.y == 0)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);          //NPC nin hareketi      Physics2D.Raycast(merkez, doðrultu, mesafe,hangi layer a deðeceði);
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);       //LayerMask.GetMask("Ground")
            if (groundInfo.collider == false)
            {
                if (movingRight)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;

                    Vector3 Scaler = transform.localScale;
                    Scaler.z *= -1;
                    transform.localScale = Scaler;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;

                    Vector3 Scaler = transform.localScale;
                    Scaler.z *= -1;
                    transform.localScale = Scaler;
                }
            }

            RaycastHit2D groundHorizontal = Physics2D.Raycast(groundDetectionH.position, Vector2.right, 0.1f);
            if (groundHorizontal.collider == true)
            {
                if (movingRight)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;

                    Vector3 Scaler = transform.localScale;
                    Scaler.z *= -1;
                    transform.localScale = Scaler;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;

                    Vector3 Scaler = transform.localScale;
                    Scaler.z *= -1;
                    transform.localScale = Scaler;
                }
            }
        }
    }
}
