using UnityEngine;
using TMPro;

public class GoldScript : MonoBehaviour
{
    [HideInInspector] public EnvanterButton eb;
    [HideInInspector] public int coinCount;
    public GameObject coinText;
    public static bool textIsActive;
    public static int coinCountS;

    AudioManager audioManager;

    private void Start()
    {
        eb = GameObject.Find("EnvanterBackground").GetComponent<EnvanterButton>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        textIsActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int a = Random.Range(0, 3);
            if (coinCount > 49)
            {
                audioManager.playSound("coinBuy2");
            }
            else
            {
                if (a == 0)
                    audioManager.playSound("coinOne1");
                else if (a == 1)
                    audioManager.playSound("coinOne2");
                else
                    audioManager.playSound("coinOne3");
            }

            coinCountS += coinCount;
            if (!textIsActive & !GameObject.Find("CoinText(Clone)"))
            {
                textIsActive = true;

                GameObject text = Instantiate(coinText, transform.position, Quaternion.identity);
                text.GetComponent<TextMeshPro>().text = coinCount.ToString();
            }

            eb.ourCoin += coinCount;
            Destroy(gameObject);
        }
    }
}
