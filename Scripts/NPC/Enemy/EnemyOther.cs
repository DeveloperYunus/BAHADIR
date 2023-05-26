using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using EZCameraShake;

public class EnemyOther : MonoBehaviour
{
    public string enemyGroupName;
    [HideInInspector] public bool canDamage;                                                           //hasar alabilirmi
    public static float staticDamage; 
    Rigidbody2D rb;
    PlayerController playerC;

    public Animator enemyAnimator;
    public float MaxHealth;
    public float FArmor, MArmor;
    public float damage;
    public float attackSpeed;
    public bool throwIce;
    public GameObject enemyHP, bar, barBack, HPText;
    float health, hasarForFlyText;

    [HideInInspector] public float slow;
    [HideInInspector] public float damageTypeS;
    [HideInInspector] public float enemyAnimatorSpeed;
    int stun;
    public static float transformForIce;

    [Header("ParticleEffect")]
    public ParticleSystem bloodParticle;
    public bool specialParticle;
    public Color firstColor,secondColor;
    public GameObject flyDamageText;

    AudioManager audioManager;


    private void Start()
    {
        HPText.GetComponent<TextMeshPro>().DOFade(0, 0);
        enemyHP.GetComponent<SpriteRenderer>().DOFade(0, 0);
        bar.GetComponent<SpriteRenderer>().DOFade(0, 0);
        barBack.GetComponent<SpriteRenderer>().DOFade(0, 0);

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        if (GetComponent<EnemyRangedAII>())
        {
            if (throwIce)
                damageTypeS = 3;
            else
                damageTypeS = 2;
        }
        else
            damageTypeS = 1;


        playerC = GameObject.Find("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        enemyAnimator.speed = 0.7f;
        health = MaxHealth;
        slow = 1;
        enemyAnimatorSpeed = 1;
        canDamage = true;
    }
    private void FixedUpdate()// burasý eskýden Update idi optimizasyon yaparken fixedUpdate yaptýk.
    {
        enemyAnimator.speed = 0.7f * slow * enemyAnimatorSpeed;
        enemyAnimator.SetFloat("speedx", Mathf.Abs(rb.velocity.x));
        enemyAnimator.SetFloat("speedy", rb.velocity.y);

        if (transform.localScale.x > 0) 
            HPText.transform.localScale = new Vector3(Mathf.Abs(HPText.transform.localScale.x), HPText.transform.localScale.y);
        else
            HPText.transform.localScale = new Vector3(-1f * Mathf.Abs(HPText.transform.localScale.x), HPText.transform.localScale.y);
    }
  

    public void EnemyGetDamage(float amount, int damageType,Transform bulletOrSword)
    {        
        if (canDamage)
        {
            //ses ayarý
            if (damageType == 1)
            {
                int a = Random.Range(0, 2);
                if (a == 0) audioManager.playSound("cut1");
                else if (a == 1) audioManager.playSound("cut2");
            }

            //geri sekme için
            if (damageType != 4)// taþ carpýnca geri sekme olmasýn
            {
                if (transform.position.x > bulletOrSword.position.x)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * 100f);
                }
                else
                {
                    GetComponent<Rigidbody2D>().AddForce(-Vector2.right * 100f);
                }
            }

            stun++;
            if (damageType == 3)//buztopu yediði zaman
            {
                slow = 0f;
                StartCoroutine(SlowEnemyCD(2f));
                transformForIce = transform.localScale.x;
            }
            else if (damageType == 4)//yerden cýkan kazýklar
            {
                stun--;
                enemyAnimator.Play("Hurt");
            }
            else if (damageType == 5)//düþen taþlar
            {
                stun--;
                slow = 0.4f;
                StartCoroutine(SlowEnemyCD(2f));
                enemyAnimator.Play("Hurt");
            }
            else if (stun == 5) 
            {
                stun = 0;
                slow = 0.2f;
                enemyAnimator.Play("Hurt");
                StartCoroutine(SlowEnemyCD(0.4f));
            }
            else
            {
                slow = 0.6f;
                StartCoroutine(SlowEnemyCD(0.2f));
            }

            CameraShaker.Instance.ShakeOnce(3f, 4, 0.1f, 1);

            HPText.GetComponent<TextMeshPro>().DOFade(1, 0.5f);
            enemyHP.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
            bar.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
            barBack.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);         

            if (damageType == 1 | damageType == 4 | damageType == 5)//fiziksel hasar
            {
                canDamage = false;
                StartCoroutine(CanDamageTimer());
                if (GetComponent<DropGold>().eb.lastSwordID > 0 && GetComponent<DropGold>().eb.lastSwordID < 6)
                {
                    hasarForFlyText = (amount - amount * ((FArmor - FArmor * (GetComponent<DropGold>().eb.lastSwordID * 3 + playerC.playerData.canCalma) / 100) / 100));
                }
                else
                {
                    hasarForFlyText = (amount - amount * ((FArmor - FArmor * (playerC.playerData.canCalma) / 100) / 100));
                }
                playerC.BloodDrain(hasarForFlyText);
            }
            else if (damageType == 2 | damageType == 3) //büyü hasarý
            {
                hasarForFlyText = (amount - amount * (MArmor / 100));
            }

            health -= hasarForFlyText;
            enemyHP.GetComponent<Transform>().localScale = new Vector3((health / MaxHealth), enemyHP.transform.localScale.y, enemyHP.transform.localScale.z);
            HPText.GetComponent<TextMeshPro>().text = ((int)health).ToString();
            StartCoroutine(EnemyHPBarFade(health));

            var flyDText = Instantiate(flyDamageText, new Vector3(transform.position.x + Random.Range(-0.4f, 0.4f),
                                transform.position.y + Random.Range(0.4f, 0.8f), transform.position.z), Quaternion.identity);            
            flyDText.GetComponent<TextMeshPro>().text = (Mathf.Floor(hasarForFlyText) + ((Mathf.Round(hasarForFlyText * 10)) % 10) / 10).ToString();

            if (health <= 0.7f)            
                EnemyDie();          
        }
    }
    public void EnemyPlayOneAnimation(string isim)                                 //bütün tekli animasyonlar burda olacak hangisini istersek burdan cagýracaz
    {       
        switch (isim)
        {
            case "attack":
                enemyAnimator.Play("Attacking");
                break;
            case "taunt":
                enemyAnimator.Play("Taunt");
                break;
        }       
    }
    public void EnemyDie()
    {
        switch (enemyGroupName)
        {
            case "blood":
                int a = Random.Range(0, 2);
                if (a == 0) audioManager.playSound("enemyBloodDie1");
                else audioManager.playSound("enemyBloodDie2"); 
                break;

            case "tree":
                audioManager.playSound("enemyWoodDie1");
                break;

            case "rock":
                int b = Random.Range(0, 2);
                if (b == 0) audioManager.playSound("enemyRockDie1");
                else audioManager.playSound("enemyRockDie2");
                break;

            case "mage":
                audioManager.playSound("enemyMageDie1");

                int c = Random.Range(0, 2);
                if (c == 0) audioManager.playSound("enemyBloodDie1");
                else audioManager.playSound("enemyBloodDie2");
                break;
        }


        GetComponent<DropGold>().DropGoldFunc();
        var bloodPaarticleVar = Instantiate(bloodParticle, transform.position, Quaternion.Euler(-90, 0, 0));
        if (!specialParticle)
        {
            var mainVar = bloodPaarticleVar.main;
            var mainSmallVar = bloodPaarticleVar.transform.GetChild(0).GetComponent<ParticleSystem>().main;

            mainVar.startColor = new ParticleSystem.MinMaxGradient(firstColor, secondColor);
            mainSmallVar.startColor = new ParticleSystem.MinMaxGradient(firstColor, secondColor);
        }

        canDamage = true;
        Destroy(gameObject);

        if(GetComponent<IsDestroyPPrefs>())
        {
            GetComponent<IsDestroyPPrefs>().Die();
        }
    }

    IEnumerator SlowEnemyCD(float wait)
    {
        yield return new WaitForSeconds(wait);
        slow = 1f;
    }
    IEnumerator CanDamageTimer()
    {
        yield return new WaitForSeconds(0.4f);
        canDamage = true;
    }
    IEnumerator EnemyHPBarFade(float currentHealth)
    {
        yield return new WaitForSeconds(3.5f);
        if (currentHealth == int.Parse(HPText.GetComponent<TextMeshPro>().text))
        {
            HPText.GetComponent<TextMeshPro>().DOFade(0, 0.5f);
            enemyHP.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
            bar.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
            barBack.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
        }
    }

}
