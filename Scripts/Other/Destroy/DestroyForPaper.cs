using UnityEngine;

public class DestroyForPaper : MonoBehaviour
{
    public ParticleSystem psDestruct;
    public GameObject paperPrefab;
    public string[] paperString;
    public float readTime, buildPaperWaitTime;

    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) 
        {
            int a = Random.Range(0, 2);
            if (a == 0)
                audioManager.playSound("crate3");
            else
                audioManager.playSound("crate4");

            if (other.transform.GetComponent<Bullet>())
            {
                //menzýllý saldýrýlar carpýnca atestopu buztopu vs. yokedilmesi iþlemi
                other.transform.GetComponent<Bullet>().PlayerBulletProcedur(other.gameObject, true);
                other.transform.GetComponent<Bullet>().Triggered();
            }

            Instantiate(psDestruct, new Vector3(transform.position.x + 0.4f, transform.position.y, transform.position.z), Quaternion.identity);

            GameObject paper = Instantiate(paperPrefab, new Vector3(transform.position.x, transform.position.y,
                               transform.position.z), paperPrefab.transform.rotation);

            paper.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-100, 100), Random.Range(130, 80)));
            paper.GetComponent<Paper>().loreText = paperString[PlayerPrefs.GetInt("language")];
            paper.GetComponent<Paper>().time = readTime;
            paper.GetComponent<Paper>().IEnumTime = buildPaperWaitTime;

            Destroy(gameObject, 0.07f);
        }
    }
}
