using UnityEngine;
using DG.Tweening;

public class DropGold : MonoBehaviour
{
    public EnvanterButton eb;
    public float axisY;

    public GameObject goldObject;
    public int minFallCoin, maxFallCoin;
    public int minValue, maxValue;
    public float minDropForce, maxDropForce;

    public void DropGoldFunc()
    {
        int a = Random.Range(minFallCoin, maxFallCoin+1);
        for (int i = 0; i < a; i++)
        {
            GameObject coin = Instantiate(goldObject, new Vector3(transform.position.x, transform.position.y + axisY, transform.position.z), Quaternion.identity);
            coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10f, 10f), Random.Range(1.1f, 0.3f) * Random.Range(minDropForce, maxDropForce)));
            int b = Random.Range(minValue, maxValue + 1);
            if (coin.GetComponent<GoldScript>())
            { 
                coin.GetComponent<GoldScript>().eb = eb;
                coin.GetComponent<GoldScript>().coinCount = b;
            }
            if (b < 3)
                coin.GetComponent<Transform>().DOScale(0.2f, 0f);
            else if (b < 6)
                coin.GetComponent<Transform>().DOScale(0.3f, 0f);
            else if (b > 5)
                coin.GetComponent<Transform>().DOScale(0.4f, 0f);

        }
    }
}
