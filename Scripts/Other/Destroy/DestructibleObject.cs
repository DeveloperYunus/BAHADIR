using UnityEngine;
using DG.Tweening;

public class DestructibleObject : MonoBehaviour
{
    public EnvanterButton eb;
    public GameObject barrelPS;
    public float axisY;
    public float destroyScale;
    public bool isBarrelForSound;

    [Header("")]
    public GameObject goldObject;
    public bool goldChest;
    public int minCoinCount, maxCoinCount;
    public int minValue, maxValue;
    public float minDropForce, maxDropForce;

    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            DestroyThis();
            if (other.transform.GetComponent<Bullet>())
            {
                //menzýllý saldýrýlar carpýnca atestopu buztopu vs. yokedilmesi iþlemi
                other.transform.GetComponent<Bullet>().PlayerBulletProcedur(other.gameObject, true);
                other.transform.GetComponent<Bullet>().Triggered();
            }
            else if (other.transform.GetComponent<EnemyBullet>())
            {
                other.transform.GetComponent<EnemyBullet>().Triggered();
            }
        }
    }

    void DestroyThis()
    {
        int a = Random.Range(0, 2);
        //barrel ise 
        if (isBarrelForSound)
        {
            if (a == 0)
                audioManager.playSound("crate3");
            else
                audioManager.playSound("crate4");
        }
        else //sandýk ise
        {
            if (a == 0)
                audioManager.playSound("crate1");
            else
                audioManager.playSound("crate2");
        }

        GetComponent<Transform>().DOScale(destroyScale, 0.07f);
        Instantiate(barrelPS, new Vector3(transform.position.x, transform.position.y + axisY, transform.position.z), Quaternion.identity);
        DropGold();
        Destroy(gameObject, 0.09f);
    }

    void DropGold()
    {
        int a = Random.Range(minCoinCount, maxCoinCount);
        for (int i = 0; i < a; i++)
        {
            GameObject coin = Instantiate(goldObject, new Vector3(transform.position.x, transform.position.y + axisY, transform.position.z), Quaternion.identity);
            coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10f, 10f), Random.Range(1.1f, 0.3f) * Random.Range(minDropForce, maxDropForce)));
            coin.GetComponent<GoldScript>().eb = eb;
            int b = Random.Range(minValue, maxValue);
            coin.GetComponent<GoldScript>().coinCount = b;
            if (b < 3)
                coin.GetComponent<Transform>().DOScale(0.2f, 0f);
            else if(b<6) 
                coin.GetComponent<Transform>().DOScale(0.3f, 0f);
            else if (b > 5)
                coin.GetComponent<Transform>().DOScale(0.4f, 0f);

            if(minValue>40)//elmas için
                coin.GetComponent<Transform>().DOScale(0.7f, 0f);
        }
    }
}
