using UnityEngine;
using TMPro;
using DG.Tweening;

public class LorePaper : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] strings;
    [HideInInspector] public int stringNumber;

    private void Start()
    {
        stringNumber = 0;
    }

    public void ForwardString()
    {
        stringNumber++;
        if (stringNumber > (strings.Length - 1)) stringNumber = strings.Length - 1;
        else
        {
            text.DOFade(0, 0.3f).OnComplete(Text);
            text.DOFade(0.9f, 0.5f).SetDelay(0.3f);
        }
    }
    public void BackString()
    {
        stringNumber--;
        if (stringNumber < 0) stringNumber = 0;
        else
        {
            text.DOFade(0, 0.3f).OnComplete(Text);
            text.DOFade(0.9f, 0.5f).SetDelay(0.3f);
        }
    }

    void Text()
    {
        text.text = strings[stringNumber];
    }
    public void ExitPage()
    {
        GetComponent<RectTransform>().DOScale(1, 0f).SetDelay(0.5f);
        GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
    }
}
