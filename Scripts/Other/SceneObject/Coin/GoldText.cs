using UnityEngine;
using TMPro;

public class GoldText : MonoBehaviour
{
    float timer, zaman;
    TextMeshPro text;


    void Start()
    {
        timer = 1f;
        text = GetComponent<TextMeshPro>();
    }
    void Update()
    {
        if (Time.time >= zaman)
        {
            if (int.Parse(text.text) != GoldScript.coinCountS)
            {
                timer = 1f;
            }

            if (timer > 0)
            {
                timer -= Time.deltaTime;
                text.text = GoldScript.coinCountS.ToString();
                if (timer <= 0)
                {
                    Destroy(gameObject);
                    GoldScript.textIsActive = false;
                    GoldScript.coinCountS = 0;
                }
            }
            zaman = Time.time + 0.05f;
        }
    }
}
