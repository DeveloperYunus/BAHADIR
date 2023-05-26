using System.Collections;
using UnityEngine;
using DG.Tweening;

public class FallingRockTrap : MonoBehaviour
{
    public GameObject damagedRock;
    public GameObject gibbet;
    public Sprite cuttedRopeVersion;
    public ParticleSystem destroyRockPS;
    public GameObject stonePiece;

    [Header("Stones")]
    public float damageAmount;
    public float minRockAmount, maxRockAmount;
    public float minOran, maxOran;
    public float addForcePozitifX, addForceNegatifX;              //normalý 95 iyidirz
    public float liveTime;
    public Sprite[] stoneSprite;

    [Header("Sounds")]
    public AudioSource rope;
    public AudioSource rockSmashing1, rockSmashing2;

    bool oneTime;

    private void Start()
    {
        oneTime = true;
    }
    public void RocksStartFalll()
    {
        if (oneTime)
        {
            oneTime = false;
            rope.Play();
            gibbet.GetComponent<SpriteRenderer>().sprite = cuttedRopeVersion;
            StartCoroutine(Rock());
        }
    }

    IEnumerator Rock()
    {
        yield return new WaitForSeconds(0.4f);
        int aa = Random.Range(0,2);
        if (aa == 0) rockSmashing1.Play();
        else rockSmashing2.Play();

        destroyRockPS.Play();
        damagedRock.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        float aaa = Random.Range(minRockAmount, maxRockAmount + 1);
        for (int i = 0; i < aaa; i++) 
        {
            GameObject a = Instantiate(stonePiece, new Vector3(damagedRock.transform.position.x, damagedRock.transform.position.y + Random.Range(0.6f, 0.1f),
                transform.position.z), Quaternion.identity, transform);
            a.GetComponent<SpriteRenderer>().sprite = stoneSprite[Random.Range(0, 3)];
            float b = Random.Range(minOran, maxOran);
            a.GetComponent<Transform>().DOScale(b, 0);
            a.GetComponent<StoneForRockTrap>().damage = Mathf.Round(damageAmount * b);
            a.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(addForcePozitifX, addForceNegatifX), Random.Range(180f, 70f)));
            a.GetComponent<Rigidbody2D>().AddTorque(Random.Range(25, -25));
            a.GetComponent<StoneForRockTrap>().liveTime = liveTime;
        }
        
    }
}
