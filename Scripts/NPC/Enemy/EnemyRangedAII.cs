using System.Collections;
using UnityEngine;
using Pathfinding;

public class EnemyRangedAII : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public float activeDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200f;
    public float attackDistance;
    [HideInInspector]public float nextWaypointDistance = 3f;
    public float jumpHeight = 0.8f;
    public float jumpForce = 100f;
    public float jumpCheckRadius = 0.1f;
    public Transform groundCheckPos;
    public LayerMask whatIsGround;   

    Path path;
    Seeker seeker;
    Vector2 startPos;
    Rigidbody2D rb;
    EnemyOther enemyOther;
    float attackSpeed;
    [HideInInspector] public float enemyNewSpeed;

    int currentWaypoint = 0;
    float leftOrRightToHead;                       //kafa nereye bakýyor
    bool jumpEnabled = true;                       //zýplayabilirmi
    bool isGrounded = false;                       //yere deðiyormu
    bool IseePlayer = false;                         //dusman hedefi göremiyo
    bool enumeratorBool = false;                   //enumeratorde true yapýlacak ve 0.5s de bir kontrol edilmesi saðlanacak
    bool linecastTouchPlayer = false;              //enemyden cýkan ýsýn playera carpýyomu 
    bool takipEdiyom;
    bool canAttack = true;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
        canAttack = true;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        enemyOther = GetComponent<EnemyOther>();
        attackSpeed = enemyOther.attackSpeed;
        startPos = new Vector2(transform.position.x, transform.position.y);
        leftOrRightToHead = transform.localScale.x;
        nextWaypointDistance = attackDistance;

        enemyNewSpeed = 1;

        //adý yazýlan fonksiyonu belirli bir süre zamanla tekrarlar
        InvokeRepeating(nameof(UpdatePath), 0f, pathUpdateSeconds);
    }
    private void FixedUpdate()
    {
        if (target && Vector2.SqrMagnitude(target.position - transform.position) < activeDistance * activeDistance * 1.3f)
        {
            PathFollow();

            if (PlayerController.isDead)
            {
                target = null;
            }
            else if (!target && !PlayerController.isDead)
            {
                target = GameObject.Find("Player").transform;
            }

#if UNITY_EDITOR
            if (target)
                Debug.DrawLine(transform.position + new Vector3(0f, -0.5f, 0), target.position + new Vector3(0, 0.5f, 0));
#endif
        }
    }

    void UpdatePath()//yol bulma kodlarý
    {
        if (target && Vector2.SqrMagnitude(target.position - transform.position) < activeDistance * activeDistance * 1.3f)
        {
            if (EnemyAII.canIChasePlayer && Vector2.Distance(transform.position, target.position) < attackDistance && linecastTouchPlayer)
            {
                nextWaypointDistance = attackDistance;
                IseePlayer = true;
                enumeratorBool = false;
                //takip tekrar baslayýnca enumaratordeki fonksiyonlar dursun
                takipEdiyom = true;

                if (!isActiveAndEnabled)
                    return;
                Attack();
            }
            //dusman görüs mesafesinde = kovala
            else if (EnemyAII.canIChasePlayer && TargetInSeeDistance() && seeker.IsDone())
            {
                nextWaypointDistance = 2f;
                seeker.StartPath(rb.position, target.position, OnPathComplete);
                IseePlayer = true;
                enumeratorBool = false;
                //takip tekrar baslayýnca enumaratordeki fonksiyonlar dursun
                takipEdiyom = true;
            }
            //player görüs mesafesinden cýktý = baslangýc noktana dön
            else
            {
                nextWaypointDistance = 1f;
                takipEdiyom = false;
                //tek seferlik gercekleþsin diye playerRun(rakipKactý) deðiþkenini yazdým
                if (IseePlayer)
                {
                    if (!isActiveAndEnabled)
                        return;
                    StartCoroutine(IDontSeeTarget());
                    IseePlayer = false;
                }
                if (enumeratorBool)
                {
                    seeker.StartPath(rb.position, startPos, OnPathComplete);
                }
            }

            //karakteri görüyomuyum yoksa görmüyommu (bir collider a deðerse true döner)
            RaycastHit2D checkWall = Physics2D.Linecast(transform.position + new Vector3(0f, -0.5f, 0), target.position + new Vector3(0, 0.5f, 0));

            if (checkWall && checkWall.transform.CompareTag("Player"))
            {
                linecastTouchPlayer = true;
            }
            else
            {
                linecastTouchPlayer = false;
            }
        }
    }   
    void PathFollow()//yolu takip etme kodlarý
    {
        if (path == null)
            return;
        
        //baslangýc noktasýna nextwaypoint den daha az yakýn olunca yapýlacaklar
        if (Vector2.Distance(startPos, transform.position) < nextWaypointDistance && !takipEdiyom)//bir hata varsa buraya bakýlabilir !takipediyom if' i transform.location un basýna alýnýr
        {
            enumeratorBool = false;
            transform.localScale = new Vector3(leftOrRightToHead, transform.localScale.y, transform.localScale.z);
        }

        //yolun sonuna ulastýmý ?(ama biraz farklý iþliyor yolun sonunda olsan bile true döndürgüðü oluyor)
        if (currentWaypoint >= path.vectorPath.Count)
            return; 

        //herhangi bir collider a carpýyormu
        isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, jumpCheckRadius, whatIsGround);
        if (target!=null && !TargetInAttackDistance())
        {
            //yönü hesapla (Vector2 force = direction * speed * Time.deltaTime;)ucan yaratýklar icin
            //rb.AddForce(new Vector2(a*Mathf.Sqrt(Mathf.Pow(force.x, 2)+ Mathf.Pow(force.y, 2)), 0));
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            
            if (jumpEnabled && isGrounded)            
                if (direction.y > jumpHeight)
                {
                    rb.AddForce(Vector2.up * jumpForce);
                    
                    jumpEnabled = false;
                    StartCoroutine(ActivateJump());
                }

            if (direction.x == Mathf.Abs(direction.x))            
                rb.AddForce(new Vector2(speed * GetComponent<EnemyOther>().slow * enemyNewSpeed, 0));                          
            else 
                rb.AddForce(new Vector2(-speed * GetComponent<EnemyOther>().slow * enemyNewSpeed, 0));

            if (rb.velocity.x > 0.08f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (rb.velocity.x < -0.08f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        //bir sonraki yol noktasý
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
            currentWaypoint++;              
    }
    void Attack()//saldýrýr fonksiyonu bazý hatalara sebebiyet verdiðinden(sadece tek yakýn dovuscunun vurmasý gibi) farklý bir yol izlenmistir
    {
        //dusman vurus mesafesinde = saldýr
        if (takipEdiyom && TargetInAttackDistance() && canAttack)
        {
            EnemyOther.staticDamage = enemyOther.damage;

            if (nextWaypointDistance>5)//demektirki bu menzilli bir dusman
            {
                ShootWaepon sw = GetComponent<ShootWaepon>();
                if (GetComponent<EnemyOther>().throwIce)
                {
                    if (sw != null) sw.ThrowIceForEnemy();
                }
                else
                {
                    if (sw != null) sw.ThrowFireForEnemy();
                }
            }
            
            canAttack = false;
            enemyOther.EnemyPlayOneAnimation("attack");
            StartCoroutine(ActivateAttack());

            //saldýrýrken playere baksýn
            if (transform.position.x - target.position.x < 0)
                transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else 
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
  
    bool TargetInSeeDistance()//hedef görüþ alanýmdamý deðilmi
    {
        return Vector2.SqrMagnitude(transform.position - target.transform.position) < activeDistance * activeDistance;
    }
    bool TargetInAttackDistance()//hedef saldýrý alanýmdamý deðilmi
    {
        return Vector2.SqrMagnitude(transform.position - target.position) < nextWaypointDistance * nextWaypointDistance;        //bu daha optimize
    }
    void OnPathComplete(Path p)//yol esitleyici
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    IEnumerator ActivateJump()//zýplama sýklýðý
    {
        yield return new WaitForSeconds(0.8f);
        jumpEnabled = true;
    }
    IEnumerator ActivateAttack()
    {
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }
    IEnumerator IDontSeeTarget()//hedefi kovalarken gözden kayboldugunda yapýlacaklar
    {
        int a = Random.Range(1, 4);
        int b = Random.Range(1, 3);
        switch (a)
        {
            case 1:// 1s dur ------ basa dön
                leftOrRightToHead = transform.localScale.x;                                         //dusman sola mý saga mý bakarken playere kaybetti
                seeker.StartPath(rb.position, rb.position, OnPathComplete);
                if (!takipEdiyom) yield return new WaitForSeconds(2f);

                if (!takipEdiyom)
                {
                    rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
                    yield return new WaitForSeconds(1f);
                }

                if (b == 2 & !takipEdiyom)
                {
                    enemyOther.EnemyPlayOneAnimation("taunt");                              //meydan okuma animasyonunu calýstýrýr
                    yield return new WaitForSeconds(0.6f);
                }
                enumeratorBool = true;
                break;

            case 2:// 1s dur + 0.5s arkaya bak + 1s öne bak ------ basa dön
                leftOrRightToHead = transform.localScale.x;
                seeker.StartPath(rb.position, rb.position, OnPathComplete);
                if (!takipEdiyom) yield return new WaitForSeconds(1f);

                if (!takipEdiyom)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    yield return new WaitForSeconds(0.5f);
                }

                if (!takipEdiyom)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    yield return new WaitForSeconds(1.5f);
                }

                if (b == 2 & !takipEdiyom)
                {
                    enemyOther.EnemyPlayOneAnimation("taunt");                              
                    yield return new WaitForSeconds(0.6f);
                }
                enumeratorBool = true;
                break;

            case 3:// 1s dur + 0.7s ilerle ------ basa dön
                leftOrRightToHead = transform.localScale.x;
                seeker.StartPath(rb.position, rb.position, OnPathComplete);
                if (!takipEdiyom) yield return new WaitForSeconds(1f);

                if (!takipEdiyom)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        if (transform.localScale.x < 0) rb.velocity = new Vector2(2f, rb.velocity.y);                        //surtunmeden dolayý güc kaybý yasýyor
                        else rb.velocity = new Vector2(-2f, rb.velocity.y);
                        yield return new WaitForSeconds(0.05f);
                    }
                }

                seeker.StartPath(rb.position, rb.position, OnPathComplete);
                if (!takipEdiyom) yield return new WaitForSeconds(2f);

                if (b == 2 & !takipEdiyom)
                {
                    enemyOther.EnemyPlayOneAnimation("taunt");
                    yield return new WaitForSeconds(0.6f);
                }
                enumeratorBool = true;
                break;
        }
    }
}
