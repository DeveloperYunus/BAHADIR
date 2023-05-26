using UnityEngine;
using DG.Tweening; 

public class PlayerContOBase : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public int extraJumpValue;
    float moveInput;
    int extraJump;                                                              //hareket sistemleri  
    bool facingRight;
    bool isGrounded, oneFallSound;
    Rigidbody2D rb;

    public Animator playerAnimator;  
    public Transform groundCheck;                                                          //yere deðme ve zýplamak için
    public LayerMask whatIsGround;
    public float checkRadius;
    bool goRight, goLeft;

    AudioManager audioManager;
    float footStepTimer;
    public GameObject soilParticle;
    ParticleSystem.EmissionModule soilParticleMain;


    void Start()
    {
        facingRight = true;
        extraJump = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<AudioManager>();
        oneFallSound = false;
        soilParticleMain = soilParticle.GetComponent<ParticleSystem>().emission;


        if (PlayerPrefs.GetString("whereFrom")=="Market")
        {
            GetComponent<Transform>().DOMoveX(-3.37f, 0f);
        }
        else if (PlayerPrefs.GetString("whereFrom") == "SkillTree")
        {
            GetComponent<Transform>().DOMoveX(7.05f, 0f);
            GetComponent<Transform>().DOScaleX(0.6f, 0f);
            facingRight = false;
        }
        PlayerPrefs.SetString("whereFrom", null); 
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);              //yere deðiyormu kontrolu yapar

        if (!facingRight && moveInput > 0 ) 
            FlipFace();
        else if(facingRight && moveInput <0)
            FlipFace();

        if (goRight)      
            moveInput = Mathf.Lerp(moveInput, -1, 0.4f);
        else if(goLeft)
            moveInput = Mathf.Lerp(moveInput, 1, 0.4f);
        else
            moveInput = Mathf.Lerp(moveInput, 0, 0.4f);

        if (!PlayerController.isTalking)
        {
#if UNITY_EDITOR
            moveInput = Input.GetAxis("Horizontal");
#endif
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        }
        playerAnimator.speed = 1;
        playerAnimator.SetFloat("playerSpeed", Mathf.Abs(rb.velocity.x));                                   //playerSpeed deðiþkenini karakterýn hýzýna eþitler    
    }
    void Update()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.1f && (Mathf.Abs(rb.velocity.y) < 0.2f))
            soilParticleMain.rateOverTime = 15;
        else
            soilParticleMain.rateOverTime = 0;

        if (isGrounded && Mathf.Abs(rb.velocity.x) > 0.15f)
        {
            if (footStepTimer <= 0.28f)
                footStepTimer += Time.deltaTime;
            else
            {
                int a = Random.Range(0, 2);
                if (a == 0) audioManager.playSound("footStep1");
                else audioManager.playSound("footStep2");

                footStepTimer = 0;
            }

        }

        if (!isGrounded && rb.velocity.y < -0.05f)
            oneFallSound = true;

        if (isGrounded && oneFallSound)
        {
            audioManager.playSound("fallSoil1");
            oneFallSound = false;
        }


        if (isGrounded)        
            extraJump = extraJumpValue;
   
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Jump();

        playerAnimator.SetBool("isGrounded", isGrounded);
        playerAnimator.SetFloat("speedy", (rb.velocity.y));       
    }

    public void GoRight(bool a)
    {
        if (!PlayerController.isTalking)
        {
            if (a) goLeft = true;
            else goLeft = false;
        }
    }
    public void GoLeft(bool a)
    {
        if (!PlayerController.isTalking)
        {
            if (a) goRight = true;
            else goRight = false;
        }
    }
    public void Jump()
    {
        if (!PlayerController.isTalking)
        {
            if (isGrounded)
            {
                audioManager.playSound("jump1");
                rb.velocity = Vector2.up * jumpForce;                                                            //Zýplama hareketi
            }
            else if (extraJump > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                extraJump--;
            }
        }
    }
    

    public void WhichBtnPress(GameObject a)
    {
        a.GetComponent<Animator>().SetBool("up", false);
        a.GetComponent<Animator>().SetBool("press", true);
    }
    public void WhichBtnUp(GameObject a)
    {
        a.GetComponent<Animator>().SetBool("press", false);
        a.GetComponent<Animator>().SetBool("up", true);
    }

    void FlipFace()
    {
        facingRight = !facingRight;                                            //yüzü saga sola döndürme kodu                 
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
