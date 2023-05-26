using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EZCameraShake;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public int extraJumpValue;
    public Animator playerAnimator;
    public Transform groundCheck;                                                          //yere deðme ve zýplamak için
    public float checkRadius;
    public LayerMask whatIsGround;
    public Transform reBornTransformObject;

    float moveInput;
    int extraJump;                                                              //hareket sistemleri  
    bool facingRight;
    bool isGrounded, oneFallSound;
    bool canAttack1;                                                            //saldýrý için
    Rigidbody2D rb;

    [Header("Health&Mana")]
    public EnvanterButton EB;
    public Skill playerData;
    public Slider healthBar;
    public Slider energyBar;
    public TextMeshProUGUI healthText, energyText;
    public GameObject bloodText;

    float gettedDamage;                                                         //alýnan hasarý tutar
    float bloodTotalAmount, bloodTextTimer;
    float playerHealth;
    float playerEnergy;
    float sideForDead;
    float swoshGoTimer;
    bool goRight, goLeft;
    int armour, physicalD, magicD;                                               //týlsýmlarý aldýktakn sonra ayarlanacak çarpanlar.
    
    public static float damage;
    public static int damageType;
    public static bool isDead, specialDieCase;
    public static bool isTalking;


    [Header("SkillCooldown")]
    public ParticleSystem swordTrail;
    public Image rageImage;
    public Image iceImage, protectImage, regenImage;
    public float rageTime, iceTime, protectTime, regenTime;
    public float[] skillEnergy;
    bool rageCD, iceCD, protectCD, regenCD;

    float rageSwordAttack, slow, staffRegen, quickElixirTime;
    float energyRegenTime, hpRegenTime, maxEnergy07f, maxHP07f;
    int stun, attackCombo;
    [HideInInspector] public float swordInEnemySlow;
    [HideInInspector] public bool swordInEnemy;
    [HideInInspector] public bool protectShield;                                     //true iken hasar almam 

    AudioManager audioManager;
    float footStepTimer, bloodDrainAmount, zaman;
    public static string WhatIsNameToTalkIam;
    [HideInInspector]public ParticleSystem.EmissionModule soilParticleMain;


    void Start()
    {
        extraJump = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        soilParticleMain = GetComponent<SkillManaCost>().soilParticle.GetComponent<ParticleSystem>().emission;
        soilParticleMain.rateOverTime = 0;
        canAttack1 = true;
        facingRight = true;
        swordInEnemy = false;
        isDead = false;
        specialDieCase = false;
        oneFallSound = false;
        isTalking = false;

        stun = 0;
        slow = 1;
        attackCombo = 0;
        swordInEnemySlow = 1;
        rageSwordAttack = 1;
        staffRegen = 1;
        footStepTimer = 0;
        energyRegenTime = 0;
        hpRegenTime = 0;
        SpikeDamage.playerAnimatorSpeed = 1;
        SpikeDamage.playerNewSpeed = 1;
        rageCD = false;
        protectCD = false;
        regenCD = false;
        iceCD = false;

        damageType = 2;                //burasý 6. bölümdeki 2 adamýn attýðý ateþtoplarý yuzunden yapýldý yoksa ses effect i çalýsmýyor

        HealthAndEnergyControl();
        DamageAndArmourSett();
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);              //yere deðiyormu kontrolu yapar

        if (!isDead)
        {
            hpRegenTime += Time.deltaTime;
            energyRegenTime += Time.deltaTime;
            if (energyRegenTime >= 0.1f && playerEnergy < energyBar.maxValue)
            {
                energyRegenTime = 0;
                playerEnergy += (0.31f * (playerData.canYenilenmesiPasif + 1));
                energyBar.value = playerEnergy;
            }
            if (hpRegenTime >= 0.5f && playerHealth < healthBar.maxValue)
            {
                hpRegenTime = 0;
                playerHealth += staffRegen * (0.31f * (playerData.enerjiYenilenmesiPasif + 1));
                healthBar.value = playerHealth;
            }

            if (Time.time > quickElixirTime)
            {
                quickElixirTime = Time.time + 0.5f;
                if (playerHealth < maxHP07f)
                    EB.ShowHPElixir(true);
                else EB.ShowHPElixir(false);

                if (playerEnergy < maxEnergy07f)
                    EB.ShowEnergyElixir(true);
                else EB.ShowEnergyElixir(false);
            }//quick Elixir butonlarý için
        }
        healthText.text = Mathf.Round(playerHealth).ToString();
        energyText.text = Mathf.Round(playerEnergy).ToString();

        
        if (!facingRight && moveInput > 0)        
            FlipFace();        
        else if (facingRight && moveInput < 0)
            FlipFace();

        if (goRight) moveInput = Mathf.Lerp(moveInput, -1, 0.4f);
        else if(goLeft) moveInput = Mathf.Lerp(moveInput, 1, 0.4f);
        else moveInput = Mathf.Lerp(moveInput, 0, 0.4f);

        if (Time.time > zaman)
        {
            zaman = Time.time + 0.1f;
            if (isGrounded && Mathf.Abs(rb.velocity.x) > 0.15f)            
                soilParticleMain.rateOverTime = 10;           
            else
                soilParticleMain.rateOverTime = 0;
        }


        if (!isDead && !isTalking && swoshGoTimer < 0f)
        {
#if UNITY_EDITOR
            moveInput = Input.GetAxis("Horizontal");
#endif
            rb.velocity = new Vector2(moveInput * speed * slow * swordInEnemySlow * SpikeDamage.playerNewSpeed, rb.velocity.y);
        }
        else if(isTalking | isDead)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if(swoshGoTimer >= 0f)
        {
            swoshGoTimer -= Time.deltaTime;
        }

        playerAnimator.speed = 1 * slow * swordInEnemySlow * SpikeDamage.playerAnimatorSpeed;
        playerAnimator.SetFloat("playerSpeed", Mathf.Abs(rb.velocity.x));
    }
    void Update()
    {
        if(isGrounded)
        {
            extraJump = extraJumpValue;

            if (Mathf.Abs(rb.velocity.x) > 0.15f)
            {
                if (footStepTimer <= 0.28f)
                    footStepTimer += Time.deltaTime;
                else
                {
                    int a = Random.Range(0, 2);
                    if (a == 0)
                        audioManager.playSound("footStep1");
                    else
                        audioManager.playSound("footStep2");

                    footStepTimer = 0;
                }
            }

            if (oneFallSound)
            {
                audioManager.playSound("fallSoil1");
                oneFallSound = false;
            }
        }
        else if(rb.velocity.y < -0.05f)
        {
            oneFallSound = true;
        }        
   
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Jump();
            
        if(bloodTextTimer>0)
            bloodTextTimer -= Time.deltaTime;
        else
        {
            bloodText.GetComponent<TextMeshProUGUI>().text = null;
            bloodTotalAmount = 0;
        }

        playerAnimator.SetBool("isGrounded", isGrounded);
        playerAnimator.SetFloat("speedy", (rb.velocity.y));

        //yeteneklerin bekleme süreleri
        if(rageCD)
        {
            rageImage.fillAmount -= 1 / rageTime * Time.deltaTime;
            if(rageImage.fillAmount<=0)
            {
                rageImage.fillAmount = 0;
                rageCD = false;
            }
        }
        if (iceCD)
        {
            iceImage.fillAmount -= 1 / iceTime * Time.deltaTime;
            if (iceImage.fillAmount <= 0)
            {
                iceImage.fillAmount = 0;
                iceCD = false;
            }
        }
        if (protectCD)
        {
            protectImage.fillAmount -= 1 / protectTime * Time.deltaTime;
            if (protectImage.fillAmount <= 0)
            {
                protectImage.fillAmount = 0;
                protectCD = false;
            }
        }
        if (regenCD)
        {
            regenImage.fillAmount -= 1 / regenTime * Time.deltaTime;
            if (regenImage.fillAmount <= 0)
            {
                regenImage.fillAmount = 0;
                regenCD = false;
            }
        }
    }

    public void HealthAndEnergyControl()
    {
        playerHealth = (100 + playerData.can) * PlayerPrefs.GetInt("150", 1);
        healthBar.maxValue = (100 + playerData.can) * PlayerPrefs.GetInt("150", 1);
        healthBar.value = (100 + playerData.can) * PlayerPrefs.GetInt("150", 1);


        playerEnergy = 100 + playerData.enerji;
        energyBar.maxValue = 100 + playerData.enerji;
        energyBar.value = 100 + playerData.enerji;

        maxHP07f = healthBar.maxValue * 0.7f;
        maxEnergy07f = energyBar.maxValue * 0.7f;
    }
    public void DamageAndArmourSett()
    {
        armour = PlayerPrefs.GetInt("151", 1);                   //týlsýmdan sonra 0.5 olmalý.
        physicalD = PlayerPrefs.GetInt("152", 1);               //týlsýmdan sonra 2 olmalý.
        magicD = PlayerPrefs.GetInt("153", 1);                  //týlsýmdan sonra 2 olmalý.
    }

    public void GoRight(bool a)
    {
        if (!isTalking && !isDead)
        {
            if (a)
                goLeft = true;
            else
                goLeft = false;
        }
    }
    public void GoLeft(bool a)
    {
        if (!isTalking && !isDead)
        {
            if (a)
                goRight = true;
            else
                goRight = false;
        }
    }
    public void Jump()
    {
        if (!isTalking && slow != 0 && !isDead) 
        {
            if (isGrounded)
            {
                audioManager.playSound("jump1");
                rb.velocity = jumpForce * slow * swordInEnemySlow * Vector2.up;                                                            //Zýplama hareketi
            }
            else if (extraJump > 0)
            {
                rb.velocity = jumpForce * slow * swordInEnemySlow * Vector2.up;
                extraJump--;
            }
        }
    }

    public void SwordAttack()
    {
        if (!isTalking && playerEnergy > EB.lastSword.energyPrice && canAttack1 && slow != 0 && !isDead)
        {
            swordTrail.Play();//kýlýc izi calýsýr
            swoshGoTimer = 0.25f;
            int a = Random.Range(0, 3);
            if (a == 0) audioManager.playSound("sword1");
            else if (a == 1) audioManager.playSound("sword2");
            else if (a == 2) audioManager.playSound("sword3");

            attackCombo++;
            if (attackCombo == 1)
            {
                playerAnimator.Play("Attack");
            }
            else if (attackCombo == 2)
            {
                playerAnimator.Play("Attack2");
                attackCombo = 0;
            }

            //kýlýcla vurunca ileri gitmek için
            if (transform.localScale.x < 0)            
                rb.AddForce(new Vector2(90f, 0f));           
            else            
                rb.AddForce(new Vector2(-90f, 0f));

            damageType = 1;
            damage = physicalD * rageSwordAttack*(EB.lastSword.physicalDamage + EB.lastSword.physicalDamage * (playerData.fizikselHasar / 100));
            canAttack1 = false;                                                   //saldýrý hýzýný ayarlamak için saldýrýlabilirlik ozelliðini kapatýr                                             
            playerEnergy -= EB.lastSword.energyPrice;                                             //mana bedelini adý yazan scriptten alýyor
            energyBar.value = playerEnergy;

            StartCoroutine(AttackSpeed1(0.45f));
        }        
    }
    public void RageSword()
    {
        if (!rageCD && slow != 0 && !isDead && !isTalking)
        {
            int a = Random.Range(0,2);
            if (a == 0) audioManager.playSound("manRoar1");
            else audioManager.playSound("manRoar2");
            StartCoroutine(RageSoundRepeat(1.2f, 4));

            CameraShaker.Instance.ShakeOnce(2f, 4, 0.1f, 14);

            if (transform.localScale.x < 0)
                Instantiate(GetComponent<SkillManaCost>().rageParticle, new Vector3(transform.position.x + 0.1f, transform.position.y + 0.42f,
                        transform.position.z), Quaternion.identity, transform);
            else
                Instantiate(GetComponent<SkillManaCost>().rageParticle, new Vector3(transform.position.x - 0.1f, transform.position.y + 0.42f,
                        transform.position.z), Quaternion.identity, transform);

            rageCD = true;
            rageImage.fillAmount = 1;

            rageSwordAttack = 1.5f;           
            StartCoroutine(RageSword(5f));
        }
    }

    public void StaffAttackFire()
    {
        if (!isTalking && playerEnergy > EB.lastSword.energyPrice && canAttack1 && slow != 0 && !isDead)
        {
            swordTrail.Play();//kýlýc izi calýsýr
            CameraShaker.Instance.ShakeOnce(3f, 1.5f, 0.1f, 0.3f);
            damageType = 2;
            damage = magicD * (EB.lastSword.magicalDamage + EB.lastSword.magicalDamage * (playerData.buyuHasari / 100));
            GetComponent<ShootWaepon>().ShootFireBall();

            playerAnimator.Play("AttackMagic");
            canAttack1 = false;                                                                                           
            playerEnergy -= EB.lastSword.energyPrice;                                             
            energyBar.value = playerEnergy;

            StartCoroutine(AttackSpeed1(0.5f));
        }
    }
    public void StaffAttackIce()
    {
        if (!isTalking && playerEnergy > (2f * EB.lastSword.energyPrice) && !iceCD && slow != 0 && !isDead)
        {
            swordTrail.Play();//kýlýc izi calýsýr
            CameraShaker.Instance.ShakeOnce(3f, 2f, 0.1f, 0.3f);
            damageType = 3;
            damage = magicD * 1.5f * (EB.lastSword.magicalDamage + EB.lastSword.magicalDamage * (playerData.buyuHasari / 100));
            GetComponent<ShootWaepon>().ShootIceBall();

            playerAnimator.Play("AttackMagic");
            iceCD = true;
            iceImage.fillAmount = 1;
            playerEnergy -= 2.5f * EB.lastSword.energyPrice;
            energyBar.value = playerEnergy;
        }
    }
    public void DontTakeDamage()
    {
        if (!protectCD && playerEnergy > skillEnergy[2] && slow != 0 && !isDead)
        {
            audioManager.playSound("barrier1");

            if (transform.localScale.x < 0)
                Instantiate(GetComponent<SkillManaCost>().protectParticle, new Vector3(transform.position.x + 0.11f, transform.position.y + 0.4f,
                        transform.position.z), Quaternion.identity, transform);
            else
                Instantiate(GetComponent<SkillManaCost>().protectParticle, new Vector3(transform.position.x - 0.11f, transform.position.y + 0.4f,
                        transform.position.z), Quaternion.identity, transform);

            protectCD = true;
            protectImage.fillAmount = 1;
            playerEnergy -= skillEnergy[2];
            energyBar.value = playerEnergy;

            protectShield = true;
            StartCoroutine(AncientShield(5f));
        }
    }
    public void StaffRegen()
    {
        if (!regenCD && playerEnergy > skillEnergy[3] && slow != 0 && !isDead)
        {
            audioManager.playSound("heal1");

            if (transform.localScale.x < 0) 
                Instantiate(GetComponent<SkillManaCost>().regenParticle, new Vector3(transform.position.x + 0.15f, transform.position.y + 0.01f,
                        transform.position.z), Quaternion.Euler(-90, 0, 0), transform);
            else 
                Instantiate(GetComponent<SkillManaCost>().regenParticle, new Vector3(transform.position.x - 0.15f, transform.position.y + 0.01f,
                        transform.position.z), Quaternion.Euler(-90, 0, 0), transform);

            regenCD = true;
            regenImage.fillAmount = 1;
            playerEnergy -= skillEnergy[3];
            energyBar.value = playerEnergy;

            staffRegen = playerData.canYenilenmesi * PlayerPrefs.GetInt("Skill10");
            StartCoroutine(StaffRegen(PlayerPrefs.GetInt("Skill10") * 2));
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDead)
        {
            if (other.CompareTag("Bullet") | other.CompareTag("EnemySword"))
            {
                if (transform.position.x < other.GetComponentInParent<Transform>().position.x && transform.localScale.x > 0)                 
                    sideForDead = -1;                
                else if (transform.position.x > other.GetComponentInParent<Transform>().transform.position.x && transform.localScale.x < 0)                
                    sideForDead = -1;               
                else                
                    sideForDead = 1;               
            }

            if (other.CompareTag("EnemySword"))
            {
                if (other.GetComponent<EnemyDamageHolderForSword>().canGiveDamage)
                {
                    PlayerGetDamage(other.GetComponent<EnemyDamageHolderForSword>().damage, other.GetComponent<EnemyDamageHolderForSword>().EO.damageTypeS);
                    other.GetComponent<EnemyDamageHolderForSword>().canGiveDamage = false;
                    other.GetComponent<EnemyDamageHolderForSword>().CanGiveDamageTimer();
                }
            }
        }
    }
    public void PlayerGetDamage(float amount, float damageType)
    {
        if (!protectShield && !isDead)
        {
            if (damageType == 1) 
            {
                int a = Random.Range(0, 2);
                if (a == 0) audioManager.playSound("cut1");
                else if (a == 1) audioManager.playSound("cut2");
            }//klýc sesi

            stun++;
            if (damageType == 3)//buztopu yediði zaman
            {
                slow = 0f;
                UIManager.stunForSlow = 0f;
                StartCoroutine(SlowPlayerCD(1.5f));
            }
            else if(damageType == 4)//yerden cýkan kazýklar
            {
                stun--;
                CameraShaker.Instance.ShakeOnce(4f, 4, 0.1f, 1);
                playerAnimator.Play("Hit");
                damageType = 1;
            }
            else if (damageType == 5)//düþen taþlar
            {
                stun--;
                slow = 0.4f;
                CameraShaker.Instance.ShakeOnce(4f, 4, 0.1f, 1);
                playerAnimator.Play("Hit");
                StartCoroutine(SlowPlayerCD(2f));
                damageType = 1;
            }
            else if (stun == 3)
            {
                CameraShaker.Instance.ShakeOnce(4f, 4, 0.1f, 1);
                stun = 0;
                slow = 0.3f;
                playerAnimator.Play("Hit");
                StartCoroutine(SlowPlayerCD(0.5f));
            }
            else
            {
                if (damageType == 6)
                {
                    stun--;
                    CameraShaker.Instance.ShakeOnce(2f, 3, 0.1f, 1);
                    damageType = 1;
                }
                else if(damageType == 7)
                {
                    stun--;
                    CameraShaker.Instance.ShakeOnce(5f, 6, 0.1f, 1);
                    damageType = 1;
                }
                else
                {
                    CameraShaker.Instance.ShakeOnce(3f, 4, 0.1f, 1);
                }

                slow = 0.8f;
                StartCoroutine(SlowPlayerCD(0.3f));
            }

            if (damageType == 1)//fiziksel hasar
            {
                gettedDamage = (amount - (amount * playerData.zýrh / 100)) * armour;
            }
            else if (damageType == 2 | damageType == 3) //büyü hasarý(ÖNEMLÝ : Bizde þu anda buyu direnci yok olunca bu kýsmý kullanýrýz)
            {
                gettedDamage = (amount - (amount * playerData.zýrh / 100)) * armour;
            }
            
            playerHealth -= gettedDamage;
            healthBar.value = playerHealth;
            healthText.text = ((int)playerHealth).ToString();

            var flyDamageText = Instantiate(GetComponent<ShootWaepon>().flyDamageText, new Vector3(transform.position.x + Random.Range(-0.4f, 0.4f),
                                transform.position.y + Random.Range(0.3f, 0.6f), transform.position.z), Quaternion.identity);

            flyDamageText.GetComponent<TextMeshPro>().text = (Mathf.Floor(gettedDamage) + ((Mathf.Round(gettedDamage * 10)) % 10) / 10).ToString();

            if (playerHealth <= 0)
            {
                Die();
                playerHealth = 0;
            }
        }
    }
    public void UseHealthPoint()
    {
        if (EB.infoItem.itemClass == 3 && slow != 0)  
        {
            playerHealth += EB.infoItem.physicalDamage;
            if (playerHealth > healthBar.maxValue)
            {
                playerHealth = healthBar.maxValue;
            }
            healthBar.value = playerHealth;
            GameManager.GameM.RemoveItemID(EB.forRemoveItemID);
        }
        else if (EB.infoItem.itemClass == 4 && slow != 0)
        {
            playerEnergy += EB.infoItem.physicalDamage;
            if (playerEnergy > energyBar.maxValue)
            {
                playerEnergy = energyBar.maxValue;
            }
            energyBar.value = playerEnergy;
            GameManager.GameM.RemoveItemID(EB.forRemoveItemID);
        }
    }
    public void UseQuickHPelixir(Item a)
    {
        if (slow != 0 && !isDead)
        {
            int b = Random.Range(0, 2);
            if (b == 0)
                audioManager.playSound("drinkPotion1");
            else
                audioManager.playSound("drinkPotion2");

            GameManager.GameM.RemoveItem(a);
            playerHealth += a.physicalDamage;
            if (playerHealth > healthBar.maxValue)
                playerHealth = healthBar.maxValue;

            healthBar.value = playerHealth;
        }
    }
    public void UseQuickEnergyelixir(Item a)
    {
        if (slow != 0 && !isDead)
        {
            int b = Random.Range(0, 2);
            if (b == 0)
                audioManager.playSound("drinkPotion1");
            else
                audioManager.playSound("drinkPotion2");

            GameManager.GameM.RemoveItem(a);
            playerEnergy += a.physicalDamage;
            if (playerEnergy > energyBar.maxValue)
                playerEnergy = energyBar.maxValue;

            energyBar.value = playerEnergy;
        }
    }

    public void BloodDrain(float a)
    {
        if (playerHealth < healthBar.maxValue && !isDead) 
        {
            if (EB.lastSwordID > 5 && EB.lastSwordID < 11)
            {
                bloodDrainAmount = a * (playerData.canCalma + (EB.lastSwordID - 5) * 3) / 100;
                playerHealth += bloodDrainAmount;
                healthBar.value += bloodDrainAmount;
            }
            else
            {
                bloodDrainAmount = a * playerData.canCalma / 100;
                playerHealth += bloodDrainAmount;
                healthBar.value += bloodDrainAmount;
            }

            if (bloodDrainAmount > 0)
            {
                bloodTextTimer = 2f;
                bloodText.GetComponent<Animator>().Play("bloodTextAnim");
                bloodTotalAmount += (Mathf.Floor(bloodDrainAmount) + ((Mathf.Round(bloodDrainAmount * 10)) % 10) / 10);
                bloodText.GetComponent<TextMeshProUGUI>().text = bloodTotalAmount.ToString();
            }

            if (playerHealth > healthBar.maxValue)
            {
                playerHealth = healthBar.maxValue;
                healthBar.value = healthBar.maxValue;
            }
            bloodDrainAmount = 0;
        }
    }

    void FlipFace()
    {
        if (!isDead && !isTalking && !swordInEnemy && slow != 0)
        {
            GetComponent<ShootWaepon>().muzzle.Rotate(0f, 180f, 0f);
            facingRight = !facingRight;                                            //yüzü saga sola döndürme kodu                 
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }
    }
    IEnumerator AttackSpeed1(float attackSpeed)
    {
        yield return new WaitForSeconds(attackSpeed);                                         //0.35 sn sonra saldýrý 1' i  saldýrýlabilir yapar
        canAttack1 = true;
    }
    IEnumerator RageSword(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        rageSwordAttack = 1;
    }
    IEnumerator SlowPlayerCD(float wait)
    {
        yield return new WaitForSeconds(wait);
        slow = 1f;
        UIManager.stunForSlow = 1f;
    }

    IEnumerator AncientShield(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        protectShield = false;
    }
    IEnumerator StaffRegen(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        staffRegen = 1;
    }
    IEnumerator RageSoundRepeat(float waitTime,int repeatAmount)
    {
        for (int i = 0; i < repeatAmount; i++)
        {
            audioManager.playSound("heartBeat1");
            yield return new WaitForSeconds(waitTime);
        }
    }

    void Die()
    {
        if (!isDead)
        {
            goLeft = false;
            goRight = false;
            playerAnimator.SetTrigger("die");
            audioManager.playSound("manDie1");
            rb.velocity = new Vector2(0f, 0f);
            if (sideForDead < 0) FlipFace();
            isDead = true;

            playerEnergy = 0;
            playerHealth = 0;
            energyBar.value = 0f;
            healthBar.value = 0f;

            if (specialDieCase)
            {
                StartCoroutine(SpecialDie(4f));
                specialDieCase = false;
            }
            else
            {
                StartCoroutine(NormalDie());
            }
        }
    }

    IEnumerator NormalDie()
    {
        GetComponent<PlayerDieZerlikTalk>().ZerlikTalk(false, "");
        yield return new WaitForSeconds(4.2f);
        LevelTransition.instance.OtherSceneAnimPlay("OBFinish");
        yield return new WaitForSeconds(0.2f);
        audioManager.playSound("sceneDark");
        yield return new WaitForSeconds(0.3f);
        transform.position = reBornTransformObject.position;
        GetComponent<PlayerController>().enabled = true;
        isDead = false;
        playerAnimator.Play("Idle");

        playerEnergy = energyBar.maxValue * 0.3f;
        energyBar.value = playerEnergy;
        playerHealth = healthBar.maxValue * 0.3f;
        healthBar.value = playerHealth;

        /*GetComponent<ShootWaepon>().muzzle.rotation = new Quaternion(0, 0, 0, Quaternion.identity.w);
        facingRight = true;
        Vector3 Scaler = transform.localScale;
        Scaler.x = -0.6f;
        transform.localScale = Scaler;*/

        yield return new WaitForSeconds(0.7f);
        LevelTransition.instance.OtherSceneAnimPlay("OBStart");
    }
    IEnumerator SpecialDie(float time)
    {
        GetComponent<DropGold>().DropGoldFunc();
        GetComponent<DropGold>().eb.ourCoin -= 90;
        GetComponent<PlayerDieZerlikTalk>().ZerlikTalk(true, "Bu bile hafif kalýr sana.");
        yield return new WaitForSeconds(4f);
        LevelTransition.instance.OtherSceneAnimPlay("OBFinish");
        yield return new WaitForSeconds(0.5f);
        audioManager.playSound("sceneDark");

        yield return new WaitForSeconds(time - 4f);
        transform.position = reBornTransformObject.position;
        jumpForce = 8.5f;
        playerHealth = energyBar.maxValue;
        energyBar.value = healthBar.maxValue;
        healthBar.value = energyBar.maxValue;
        healthText.text = healthBar.maxValue.ToString();
        energyText.text = energyBar.maxValue.ToString();
        GetComponent<PlayerDieZerlikTalk>().ZerlikTalk(true, null);

        transform.GetChild(0).GetComponent<Animator>().Play("Idle");
        isDead = false;

        yield return new WaitForSeconds(0.6f);
        LevelTransition.instance.OtherSceneAnimPlay("OBStart");
    }
}
