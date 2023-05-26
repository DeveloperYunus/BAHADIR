using UnityEngine;
using DG.Tweening;

public class ButtonAnimator : MonoBehaviour
{
    public void BtnDown(GameObject a)
    {
        a.GetComponent<RectTransform>().DOScale(0.9f, 0.1f);
    }
    public void BtnUp(GameObject a)
    {
        a.GetComponent<RectTransform>().DOScale(1.07f, 0.1f);
        a.GetComponent<RectTransform>().DOScale(1f, 0.1f).SetDelay(0.1f);
    }
}
