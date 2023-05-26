using UnityEngine;
using DG.Tweening;

public class IceSplanan : MonoBehaviour
{
    public SpriteRenderer[] particleS;
    public GameObject parent;

    void Start()
    {
        if (particleS != null)
        {
            int aa = particleS.Length;
            for (int i = 0; i < aa; i++)
            {
                float a;
                if (EnemyOther.transformForIce != 0)
                {
                    a = 1 / EnemyOther.transformForIce;
                    parent.GetComponent<Transform>().DOScale(a, 0f);
                }

                particleS[i].GetComponent<SpriteRenderer>().DOFade(0, 0.2f).SetDelay(0.8f);
            }
        }
    }
}


